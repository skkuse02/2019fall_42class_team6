var express    = require('express');
var app = express();
var server = require('http').createServer(app);
var router = require('./router/main')(app);
var io = require('socket.io')(server);

app.set('views', __dirname + '/views');
app.set('view engine', 'ejs');
app.engine('html', require('ejs').renderFile);

io.on('connection',function(socket){
  socket.on('event_name', function(data){
    console.log('Message from Client: ' + data);
  });
});

// configuration ===============================================================

app.listen(app.get('port'), function () {
  console.log('Express server listening on port ' + app.get('port'));
});