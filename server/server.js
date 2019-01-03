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
    "SELECT NhanVien.* FROM NhanVien WHERE NhanVien.maNV = '" + username + "'";

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
        if (user.matKhau.toUpperCase() == password.toUpperCase()) {
          res.json({
            code: 0,
            msg: "Dang nhap thanh cong",
            loaiNV: user.idLoaiNV,
            hoTen: user.hoTen,
            id: user.id
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

app.post("/api/updateDrink", function(req, res) {
  const id = req.body.id;
  const tenSanPham = req.body.tenSanPham;
  const gia = req.body.gia;
  const thongTin = req.body.thongTin;

  const query =
    "UPDATE SanPham SET tenSanPham = N'" +
    tenSanPham +
    "', thongTin= N'" +
    thongTin +
    "', gia=" +
    gia +
    " WHERE id = " +
    id;

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
        msg: "Cap nhat thong tin mon an thanh cong"
      });
    }
  });
});

app.get("/api/deleteDrink/:id", function(req, res) {
  const deleteDependencies =
    "DELETE FROM ChiTietHoaDon WHERE idSanPham = " + req.params.id;
  const deleteQuery = "DELETE FROM SanPham WHERE id = " + req.params.id;

  let request = new sql.Request();

  request.query(deleteDependencies, function(err, result) {
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
});

app.post("/api/putOrder", function(req, res) {
  const maHoaDon = Math.random()
    .toString(36)
    .replace(/[^a-z]+/g, "")
    .substr(0, 9); //Random
  const thoiGianLap = Math.round(moment() / 1000);
  const gia = req.body.gia;
  const idKhachHangMua = req.body.idKhachHangMua;
  const idNhanVienLap = req.body.idNhanVienLap;
  const danhSachMonAn = req.body.danhSachMonAn;
  console.log(req.body);
  var insertOrder;
  if (idKhachHangMua == "") {
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
  console.log(insertOrder);
  const getOrderId =
    "SELECT id FROM HoaDon WHERE maHoaDon = '" + maHoaDon + "'";

  let request = new sql.Request();

  request.query(insertOrder, function(err, result) {
    if (err) {
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
            const id = result[0].id;
            var successful = true;
            danhSachMonAn.forEach(monAn => {
              let insertOrderDetail =
                "INSERT INTO ChiTietHoaDon (idHoaDon, idSanPham, soLuong, size) VALUES (" +
                id +
                ", " +
                monAn.id +
                ", " +
                monAn.soLuong +
                ", '" +
                monAn.size +
                "' )";
                console.log(insertOrderDetail);
              request.query(insertOrderDetail, function(err, result) {
                if (err) {
                  console.log(err);
                  successful = false;
                  res.json({
                    code: -3,
                    msg: "Khong the ket noi den CSDL"
                  });
                }
              });
            });
            if (successful == true) {
              res.json({
                code: 0,
                msg: "Them hoa don thanh cong"
              });
            }
          }
        }
      });
    }
  });
});

app.post("/api/addCustomer", function(req, res) {
  const hoTen = req.body.hoTen;
  const ngaySinh = moment(req.body.ngaySinh, "DD-MM-YYYY") / 1000;
  const soDienThoai = req.body.soDienThoai;
  const cmnd = req.body.cmnd;
  const diemTichLuy = 0;
  const maKH = Math.random()
    .toString(36)
    .replace(/[^a-z]+/g, "")
    .substr(0, 9);

  const insertCustommer =
    "INSERT INTO KhachHang (maKH, hoTen, ngaySinh, soDienThoai, diemTichLuy, cmnd) VALUES ('" +
    maKH +
    "', N'" +
    hoTen +
    "', '" +
    ngaySinh +
    "', '" +
    soDienThoai +
    "', '" +
    diemTichLuy +
    "', '" +
    cmnd +
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
  const id = req.body.id;
  const maKH = req.body.maKH;
  const hoTen = req.body.hoTen;
  const ngaySinh = moment(req.body.ngaySinh, "DD/MM/YYYY") / 1000;
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

app.get("/api/getCustomers", function(req, res) {
  const query = "SELECT * FROM KhachHang";
  // console.log();
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
        res.json({
          code: 0,
          msg: "Thong tin khach hang da chon",
          payload: result
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
            ngaySinh: moment.unix(customer.ngaySinh).format("DD/MM/YYYY"),
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
        var customers = [];
        result.forEach(customer => {
          const dob = moment.unix(customer.ngaySinh).format("DD/MM/YYYY");
          var newCustomer = { ...customer, ngaySinh: dob };
          customers.push(newCustomer);
        });
        res.json({
          code: 0,
          msg: "Thong tin khach hang da chon",
          payload: customers
        });
      }
    }
  });
});

app.get("/api/getEmployees", function(req, res) {
  const query = "SELECT * FROM NhanVien";
  // console.log();
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
          msg: "Khong tim thay nhan vien"
        });
      } else {
        res.json({
          code: 0,
          msg: "Thong tin nhan vien da chon",
          payload: result
        });
      }
    }
  });
});

app.get("/api/getEmployeeByName/:name", function(req, res) {
  const query =
    "SELECT TOP (10) * FROM NhanVien WHERE hoTen LIKE N'" +
    req.params.name +
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
          msg: "Khong tim thay nhân viên"
        });
      } else {
        var employees = [];
        result.forEach(employee => {
          const dob = moment.unix(employee.ngaySinh).format("DD/MM/YYYY");
          var newEmployee = { ...employee, ngaySinh: dob };
          employees.push(newEmployee);
        });

        res.json({
          code: 0,
          msg: "Thong tin nhân viên da chon",
          payload: employees
        });
      }
    }
  });
});

app.get("/api/getEmployeeById/:id", function(req, res) {
  const query =
    "SELECT TOP (1) * FROM NhanVien WHERE id = " + req.params.id + ";";

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
          msg: "Khong tim thay nhân viên"
        });
      } else {
        var employees = [];
        result.forEach(employee => {
          const dob = moment.unix(employee.ngaySinh).format("DD/MM/YYYY");
          var newEmployee = { ...employee, ngaySinh: dob };
          employees.push(newEmployee);
        });

        res.json({
          code: 0,
          msg: "Thong tin nhân viên da chon",
          payload: employees
        });
      }
    }
  });
});

app.post("/api/updateEmployee", function(req, res) {
  const id = req.body.id;
  const maNV = req.body.maNV;
  const hoTen = req.body.hoTen;
  const ngaySinh = moment(req.body.ngaySinh, "DD/MM/YYYY") / 1000;

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

app.post("/api/changePassword", function(req, res) {
  const id = req.body.id;
  const oldPass = req.body.oldPass.toUpperCase();
  const newPass = req.body.newPass.toUpperCase();

  const findOldPass = "SELECT matKhau FROM NhanVien WHERE id = " + id;
  const updatePass =
    "UPDATE NhanVien SET matKhau = '" + newPass + "' WHERE id = " + id;

  let request = new sql.Request();

  request.query(findOldPass, function(err, result) {
    if (err) {
      console.log(err);
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      if (result.length == 0) {
        res.json({
          code: -4,
          msg: "Nhân viên không tồn tại"
        });
      } else {
        if (result[0].matKhau.toUpperCase() != oldPass) {
          res.json({
            code: -5,
            msg: "Mật khẩu cũ không chính xác"
          });
        } else {
          request.query(updatePass, function(err, result) {
            if (err) {
              console.log(err);
              res.json({
                code: -3,
                msg: "Co loi trong truy van CSDL"
              });
            } else {
              res.json({
                code: 0,
                msg: "Đổi mật khẩu thành công"
              });
            }
          });
        }
      }
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

app.post("/api/search", function(req, res) {
  const beginDate = req.body.BeginDate == ""? "": moment(req.body.BeginDate, "DD/MM/YYYY") / 1000;
  const endDate = req.body.EndDate == ""? "": moment(req.body.EndDate, "DD/MM/YYYY") / 1000;
  const idNhanVien = req.body.idNhanVien;
  const idKhachHang = req.body.idKhachHang == 'null'? "" : req.body.idKhachHang;

  var flag = req.body.idKhachHang == 'null';

  var query;
  if (
    beginDate == "" &&
    endDate == "" &&
    idNhanVien == "" &&
    idKhachHang == ""
  ) {
    //NULL hết
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id";
  } else if (
    beginDate == "" &&
    endDate == "" &&
    idNhanVien == "" &&
    idKhachHang != ""
  ) {
    //Null 3
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE KhachHang.id = " +
      idKhachHang;
  } else if (
    beginDate == "" &&
    endDate == "" &&
    idKhachHang == "" &&
    idNhanVien != ""
  ) {
    //NULL 3
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE NhanVien.id = " +
      idNhanVien;
  } else if (
    beginDate == "" &&
    idNhanVien == "" &&
    idKhachHang == "" &&
    endDate != ""
  ) {
    //NULL 3
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > 0 AND thoiGianLap < " +
      endDate;
  } else if (
    endDate == "" &&
    idNhanVien == "" &&
    idKhachHang == "" &&
    beginDate != ""
  ) {
    //NULL 3
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > " +
      beginDate;
  } else if (
    idNhanVien == "" &&
    idKhachHang == "" &&
    beginDate != "" &&
    endDate != ""
  ) {
    //NULL 2
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > " +
      beginDate +
      " AND thoiGianLap < " +
      endDate;
  } else if (
    idNhanVien != "" &&
    idKhachHang != "" &&
    beginDate == "" &&
    endDate == ""
  ) {
    //NULL 2
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE KhachHang.id = " +
      idKhachHang +
      " AND NhanVien.id = " +
      idNhanVien;
  } else if (
    idNhanVien != "" &&
    idKhachHang == "" &&
    beginDate != "" &&
    endDate == ""
  ) {
    //NULL 2
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > " +
      beginDate +
      " AND NhanVien.id = " +
      idNhanVien;
  } else if (
    idNhanVien == "" &&
    idKhachHang != "" &&
    beginDate == "" &&
    endDate != ""
  ) {
    //NULL 2
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap < " +
      endDate +
      " AND KhachHang.id = " +
      idKhachHang;
  } else if (
    idNhanVien != "" &&
    idKhachHang != "" &&
    beginDate != "" &&
    endDate == ""
  ) {
    //NULL 1
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > " +
      beginDate +
      " AND NhanVien.id = " +
      idNhanVien +
      " AND KhachHang.id = " +
      idKhachHang;
  } else if (
    idNhanVien != "" &&
    idKhachHang != "" &&
    beginDate == "" &&
    endDate != ""
  ) {
    //NULL 1
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap < " +
      endDate +
      " AND NhanVien.id = " +
      idNhanVien +
      " AND KhachHang.id = " +
      idKhachHang;
  } else if (
    idNhanVien != "" &&
    idKhachHang == "" &&
    beginDate != "" &&
    endDate != ""
  ) {
    //NULL 1
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > " +
      beginDate +
      " AND thoiGianLap < " +
      endDate +
      " AND NhanVien.id = " +
      idNhanVien;
  } else if (
    idNhanVien == "" &&
    idKhachHang != "" &&
    beginDate != "" &&
    endDate != ""
  ) {
    //NULL 1
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > " +
      beginDate +
      " AND thoiGianLap < " +
      endDate +
      " AND KhachHang.id = " +
      idKhachHang;
  } else {
    //NULL 0
    query =
      "SELECT maHoaDon, thoiGianLap, gia, KhachHang.hoTen as tenKhachHang, NhanVien.hoTen as tenNhanVien FROM HoaDon LEFT JOIN KhachHang ON HoaDon.idKhachHangMua = KhachHang.id JOIN NhanVien ON HoaDon.idNhanVienLap = NhanVien.id WHERE thoiGianLap > " +
      beginDate +
      " AND thoiGianLap < " +
      endDate +
      " AND KhachHang.id = " +
      idKhachHang +
      " AND NhanVien.id = " +
      idNhanVien;
  }
  let request = new sql.Request();

  request.query(query, function(err, result) {
    if (err) {
      console.log(err);
      res.json({
        code: -3,
        msg: "Co loi trong truy van CSDL"
      });
    } else {
      var danhSachHoaDon = [];
      result.forEach(hoaDon => {
        var newHD = {
          id: hoaDon.id,
          thoiGian: moment.unix(hoaDon.thoiGianLap).format("DD/MM/YYYY"),
          maHoaDon: hoaDon.maHoaDon,
          nhanVien: hoaDon.tenNhanVien,
          khachHang: hoaDon.tenKhachHang,
          gia: hoaDon.gia,
        }
        if (flag) {
          if (hoaDon.tenKhachHang == null) {
            danhSachHoaDon.push(newHD);
          }
        }
        else danhSachHoaDon.push(newHD);
      });
      res.json({
        code: 0,
        msg: "Tìm kiếm thành công",
        payload: danhSachHoaDon,
      });
    }
  });
});

app.post("/api/insertDrink", function(req, res) {
  const maSanPham = req.body.maSanPham;
  const tenSanPham = req.body.tenSanPham;
  const gia = req.body.gia;
  const thongTin = req.body.thongTin;

  const query =
    "INSERT INTO SanPham (maSanPham, tenSanPham, gia, thongTin) VALUES ('" +
    maSanPham +
    "', N'" +
    tenSanPham +
    "', '" +
    gia +
    "', '" +
    thongTin +
    "')";

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
        msg: "Them mon an thanh cong"
      });
    }
  });
});

app.get("/api/saleReport/:type", function(req, res) {
  var type = req.params.type.toUpperCase();

  if (type != "YEAR" && type != "MONTH" && type != "DAY") {
    res.json({
      code: -6,
      msg: "Loại không hợp lệ"
    });
  } else {
    const query = "SELECT " + type + "(DATEADD(S, thoiGianLap, '1970-01-01 07:00:00')) as "+type+", sum(HoaDon.gia) as DoanhThu FROM HoaDon GROUP BY "+type+"(DATEADD(S, thoiGianLap, '1970-01-01 07:00:00'))"
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
          msg: "Kết quả thống kê",
          payload: result
        });
      }
    });
  }
});
