var express = require('express'); // express module use
var path = require('path'); // path module
var logger = require('morgan'); // log module
var router = require('./routes/script'); // use router as ./routes/script.js file

var app = express();

app.use(logger('dev'));
app.use(express.json());  // use express json
app.use(express.urlencoded({ extended: false })); // use body parsing
app.use(express.static(path.join(__dirname, 'public')));
app.use(router);  // user router

app.listen(app.get('port'),function(){
  console.log('Express server listening on port ' + app.get('port'));
});
module.exports = app;