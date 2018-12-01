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

//POST API
app.post("/api/login", function (req, res) {
  let user = req.body.user;
  let pass = req.body.pass;
  console.log(user);
  console.log(pass);
  let query = "SELECT * FROM NhanVien WHERE maNV = '" + user + "'";
  let request = new sql.Request();

  request.query(query, function (err, result) {
    if (err) {
      console.log("Error while querying database :- " + err);
      res.send(err);
    }
    else {
      console.log(result);
      res.send(result);
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