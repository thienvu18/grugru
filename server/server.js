//Initiallising node modules
var express = require("express");
var bodyParser = require("body-parser");
var sql = require("mssql");
var app = express();
var moment = require("moment");

require("dotenv").config();

// Body Parser Middleware
app.use(bodyParser.json());

//CORS Middleware
app.use(function(req, res, next) {
  //Enabling CORS
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Methods", "GET,OPTIONS,POST");
  res.header(
    "Access-Control-Allow-Headers",
    "Origin, X-Requested-With, contentType,Content-Type, Accept, Authorization"
  );
  next();
});

//Setting up server
var server = app.listen(process.env.PORT || 8080, function() {
  var port = server.address().port;
});

//Initiallising connection string
var dbConfig = {
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  server: process.env.SERVER_ADDRESS,
  database: process.env.DB_NAME
};

(async function() {
  try {
    await sql.connect(dbConfig);
    console.log("Connect to db successfully");
  } catch (err) {
    console.log(err);
  }
})();

app.post("/api/login", function(req, res) {
  const username = req.body.username;
  const password = req.body.password;
  const query =
    "SELECT NhanVien.*, LoaiNhanVien.loaiNV FROM NhanVien JOIN LoaiNhanVien ON NhanVien.idLoaiNV = LoaiNhanVien.id WHERE NhanVien.maNV = '" +
    username +
    "'";

  let request = new sql.Request();

  request.query(query, function(err, result) {
    if (err) {
      res.json({
        code: -3,
        msg: "Khong the ket noi den CSDL"
      });
    } else {
      if (result.length == 0) {
        res.json({
          code: -1,
          msg: "Khong tim thay user"
        });
      } else {
        const user = result[0];
        if (user.matKhau == password) {
          console.log("ozxdzzk");
          res.json({
            code: 0,
            msg: "Dang nhap thanh cong",
            loaiNV: user.loaiNV,
            hoTen: user.hoTen
          });
        } else {
          res.json({
            code: -2,
            msg: "Dang nhap that bai"
          });
        }
      }
    }
  });
});

app.get("/api/getFoodList", function(req, res) {
  const query = "SELECT TOP (100) * FROM SanPham";

  let request = new sql.Request();

  request.query(query, function(err, result) {
    if (err) {
      console.log(err);
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      if (result.length == 0) {
        res.json({
          code: -1,
          msg: "Danh sach san pham rong"
        });
      } else {
        let coffee = [];
        let milkTea = [];
        let topping = [];

        result.forEach(food => {
          if (food.maSanPham[0] == "C") {
            coffee.push(food);
          } else if (food.maSanPham[0] == "M") {
            milkTea.push(food);
          } else if (food.maSanPham[0] == "T") {
            topping.push(food);
          }
        });
        res.json({
          code: 0,
          coffees: coffee,
          milkTeas: milkTea,
          toppings: topping
        });
      }
    }
  });
});

app.post("/api/putOrder", function(req, res) {
  const maHoaDon = Math.random()
    .toString(36)
    .replace(/[^a-z]+/g, "")
    .substr(0, 9); //Random
  const thoiGianLap = moment(req.body.ngaySinh, "DD-MM-YYYY").format(
    "YYYY-MM-DD"
  );
  const gia = req.body.gia;
  const idKhachHangMua = req.body.idKhachHangMua;
  const idNhanVienLap = req.body.idNhanVienLap;
  const danhSachMonAn = req.body.danhSachMonAn;

  var insertOrder;
  if (idKhachHangMua == null) {
    insertOrder =
      "INSERT INTO HoaDon (maHoaDon, thoiGianLap, gia, idKhachHangMua, idNhanVienLap) VALUES ('" +
      maHoaDon +
      "', '" +
      thoiGianLap +
      "', '" +
      gia +
      "', null, '" +
      idNhanVienLap +
      "')";
  } else {
    insertOrder =
      "INSERT INTO HoaDon (maHoaDon, thoiGianLap, gia, idKhachHangMua, idNhanVienLap) VALUES ('" +
      maHoaDon +
      "', '" +
      thoiGianLap +
      "', '" +
      gia +
      "', '" +
      idKhachHangMua +
      "', '" +
      idNhanVienLap +
      "')";
  }

  const getOrderId =
    "SELECT id FROM HoaDon WHERE maHoaDon = '" + maHoaDon + "'";

  let request = new sql.Request();

  request.query(insertOrder, function(err, result) {
    if (err) {
console.log(err);
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      request.query(getOrderId, function(err, result) {
        if (err) {
          res.json({
            code: -3,
            msg: "Khong the ket noi den CSDL"
          });
        } else {
          if (result.length == 0) {
            res.json({
              code: -1,
              msg: "Them hoa don that bai"
            });
          } else {
            const id = result[0];
            danhSachMonAn.forEach(monAn => {
              let insertOrderDetail =
                "INSERT INTO ChiTietHoaDon (idHoaDon, idMonAn, soLuong, size) VALUES (" +
                id +
                ", " +
                monAn.id +
                ", " +
                monAn.soLuong +
                ", '" +
                monAn.size +
                "' )";
              request.query(insertOrderDetail, function(err, result) {
                if (err) {
                  res.json({
                    code: -3,
                    msg: "Khong the ket noi den CSDL"
                  });
                }
              });
            });
            res.json({
              code: 0,
              msg: "Them hoa don thanh cong"
            });
          }
        }
      });
    }
  });
});

app.post("/api/addCustomer", function(req, res) {
  const hoTen = req.body.hoTen;
  const ngaySinh = moment(req.body.ngaySinh, "DD-MM-YYYY").format("YYYY-MM-DD");
  const soDienThoai = req.body.soDienThoai;
  const maKH = Math.random()
    .toString(36)
    .replace(/[^a-z]+/g, "")
    .substr(0, 9);

  const insertCustommer =
    "INSERT INTO KhachHang (maKH, hoTen, ngaySinh, soDienThoai) VALUES ('" +
    maKH +
    "', '" +
    hoTen +
    "', '" +
    ngaySinh +
    "', '" +
    soDienThoai +
    "')";
  const findExist =
    "SELECT 1 FROM KhachHang WHERE soDienThoai = '" + soDienThoai + "'";

  let request = new sql.Request();

  request.query(findExist, function(err, result) {
    if (err) {
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      if (result.length != 0) {
        res.json({
          code: -4,
          msg: "Khách hàng đã tồn tại"
        });
      } else {
        request.query(insertCustommer, function(err, result) {
          if (err) {
            console.log(err);
            if (
              err.message.indexOf("Violation of UNIQUE KEY constraint") != -1
            ) {
              res.json({
                code: -5,
                msg: "Lỗi không xác định"
              });
            } else
              res.json({
                code: -3,
                msg: "Co loi trong truy van CSDL"
              });
          } else {
            res.json({
              code: 0,
              msg: "Them khach hang thanh cong"
            });
          }
        });
      }
    }
  });
});

app.post("/api/updateCustomer", function(req, res) {
  console.log(req.body);
  const id = req.body.id;
  const maKH = req.body.maKH;
  const hoTen = req.body.hoTen;
  const ngaySinh = moment(req.body.ngaySinh, "DD/MM/YYYY").format("YYYY-MM-DD");
  const soDienThoai = req.body.soDienThoai;
  const diemTichLuy = req.body.diemTichLuy;
  const cmnd = req.body.cmnd;

  const query =
    "UPDATE KhachHang SET maKH = '" +
    maKH +
    "', hoTen=N'" +
    hoTen +
    "', ngaySinh='" +
    ngaySinh +
    "', soDienThoai='" +
    soDienThoai +
    "', diemTichLuy=" +
    diemTichLuy +
    ", cmnd='" +
    cmnd +
    "' WHERE id = " +
    id;
console.log(query);
  let request = new sql.Request();

  request.query(query, function(err, result) {
    if (err) {
      console.log(err);
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      res.json({
        code: 0,
        msg: "Cap nhat thong tin khach hang thanh cong"
      });
    }
  });
});

app.get("/api/deleteCustomer/:id", function(req, res) {
  const findQuery = "SELECT 1 FROM KhachHang WHERE id = " + req.params.id;
  const updateQuery =
    "UPDATE HoaDon SET idKhachHangMua = null WHERE idKhachHangMua = " +
    req.params.id;
  const deleteQuery = "DELETE FROM KhachHang WHERE id = " + req.params.id;

  let request = new sql.Request();

  request.query(findQuery, function(err, result) {
    if (err) {
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      if (result.length != 0) {
        request.query(updateQuery, function(err, result) {
          if (err) {
            res.json({
              code: -3,
              msg: "Co loi trong truy van CSDL"
            });
          } else {
            request.query(deleteQuery, function(err, result) {
              if (err) {
                res.json({
                  code: -3,
                  msg: "Co loi trong truy van CSDL"
                });
              } else {
                res.json({
                  code: 0,
                  msg: "Xoa khach hang thanh cong"
                });
              }
            });
          }
        });
      } else {
        res.json({
          code: -4,
          msg: "Khách hàng không tồn tại"
        });
      }
    }
  });
});

app.get("/api/getCustomerInfo/:id", function(req, res) {
  const query = "SELECT * FROM KhachHang WHERE id = " + req.params.id;

  let request = new sql.Request();

  request.query(query, function(err, result) {
    if (err) {
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      if (result.length == 0) {
        res.json({
          code: -1,
          msg: "Khong tim thay khach hang"
        });
      } else {
        const customer = result[0];
        res.json({
          code: 0,
          msg: "Thong tin khach hang da chon",
          payload: {
            id: customer.id,
            maKH: customer.maKH,
            hoTen: customer.hoTen,
            ngaySinh: customer.ngaySinh,
            soDienThoai: customer.soDienThoai
          }
        });
      }
    }
  });
});

app.get("/api/getCustomerByPhone/:phoneNumber", function(req, res) {
  const query =
    "SELECT TOP (10) * FROM KhachHang WHERE soDienThoai LIKE '" +
    req.params.phoneNumber +
    "%';";

  let request = new sql.Request();

  request.query(query, function(err, result) {
    if (err) {
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      if (result.length == 0) {
        res.json({
          code: -4,
          msg: "Khong tim thay khach hang"
        });
      } else {
        res.json({
          code: 0,
          msg: "Thong tin khach hang da chon",
          payload: result
        });
      }
    }
  });
});
