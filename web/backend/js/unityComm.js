var app=require('express')();
var path=require('path');
var fs = require('fs');
var mdbConn   = require('./mariaDBConn');

function socketconn(app){
  var server=require('http').Server(app);
  var io=require('socket.io')(server);
  io.sockets.on('request', function(data){
    var function_id = data.function;
    if(function_id=='GetProductfile'){
      var product_id = data.product_id;
      mdbConn.getProductfile(product_id).then((result)=>{
        for (var i in result){
          var productfile = result[i].product_file;
          var filename = path.join(__dirname,'..','public','file',productfile);
          fs.readFile(filename, function(err,buf){
            io.sockets.emit('file transfer',{file:productfile, buffer:buf});
          });
        };
      });
    };
    if(function_id=='GetRoomInfofile'){
      var model_id = data.model_id;
      mdbConn.getRoomInfofile(model_id).then((result)=>{
        var modelfile = result[0].model_file;
        var filename = path.join(__dirname,'..','puglic','file',modelfile);
        fs.readFile(filename, function(err,buf){
          io.sockets.emit('file transfer',{file:modelfile, buffer:buf});
        });
      });
    }
  });
}

module.exports={
  socketconn:socketconn
}