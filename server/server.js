//Initiallising node modules
var express = require("express");
var bodyParser = require("body-parser");
var sql = require("mssql");
var app = express();

require('dotenv').config();

// Body Parser Middleware
app.use(bodyParser.json());

//CORS Middleware
app.use(function (req, res, next) {
  //Enabling CORS 
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, contentType,Content-Type, Accept, Authorization");
  next();
});

//Setting up server
var server = app.listen(process.env.PORT || 8080, function () {
  var port = server.address().port;

  console.log("App now running on port", port);
});

//Initiallising connection string
var dbConfig = {
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  server: process.env.SERVER_ADDRESS,
  database: process.env.DB_NAME,
};

(async function () {
  try {
      await sql.connect(dbConfig);
      console.log("Connect to db successfully");
  } catch (err) {
      console.log(err);
  }
})()

app.post("/api/login", function (req, res) {
  let username = req.body.username;
  let password = req.body.password;
  let query = "SELECT * FROM NhanVien WHERE maNV = '" + username + "'";
  let request = new sql.Request();

  request.query(query, function (err, result) {
    if (err) {
      console.log("Error while querying database :- " + err);
      res.json({
        code: -3,
        msg: 'Khong the ket noi den CSDL'
      });
    }
    else {
      if (result.length == 0) {
        res.json({
          code: -1,
          msg: 'Khong tim thay user'
        });
      } else {
        const user = result[0];
        if (user.matKhau == password) {
          res.json({
            code: 0,
            msg: 'Dang nhap thanh cong'
          });
        } else {
          res.json({
            code: -2,
            msg: 'Dang nhap that bai'
          });
        }
      }
    }
  });
});

// //PUT API
// app.put("/api/user/:id", function (req, res) {
//   var query = "UPDATE [user] SET Name= " + req.body.Name + " , Email=  " + req.body.Email + "  WHERE Id= " + req.params.id;
//   executeQuery(res, query);
// });

// // DELETE API
// app.delete("/api/user/:id", function (req, res) {
//   var query = "DELETE FROM [user] WHERE Id=" + req.params.id;
//   executeQuery(res, query);
// });