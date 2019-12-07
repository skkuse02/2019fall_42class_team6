var app=require('express')();
var path=require('path');
var fs = require('fs');
var mdbConn   = require('./mariaDBConn');

function socketconn(app){
  var server=require('http').Server(app);
  var io=require('socket.io')(server);
  io.sockets.on('request', function(data){
    var function_id = data.function;
    if(function_id==''){
      var user_id = data.user_id;
      var filename = data.file_name;
      var roomname = data.json.roomname;
      var json = JSON.stringify(data.json);
      var filepath = path.join(__dirname,'..','public','file',filename);
      fs.writeFile(filepath,json, 'utf-8');
      // room save DB
      mdbConn.addModel(model_id,user_id,'',filename,roomname).then((result)={});
    };
  });
}

module.exports={
  socketconn:socketconn
}