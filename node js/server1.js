const hostname = '127.0.0.1';
const port = 3000;
const { Console } = require("console");

var http = require("http");
var querystring = require('querystring');

var messages = [];
var MessagesCapacity=5;

http.createServer(function (request, response) {

  

  if (request.method == 'GET') {
    var methodName = request.headers["method-name"];
    if (methodName == "SendMessage") {
      var m = request.headers["message-text"];
      SendMessage(m, response);
    }
    else if (methodName == "GetMessages") {
      
      GetMessages(response);

    }
  }
  else if (request.method == 'POST') {
    var index = request.url.indexOf('?')+1;
    var s = request.url.substr(index, request.url.length - index);
    var data = querystring.parse(s);

    if (data['method-name'] == "SendMessage") {
      SendMessage(data["message-text"], response);
    }
    else if (data['method-name'] == "GetMessages") {
      GetMessages(response);
    }
    
  }
  response.end();
}).listen(port);

function GetMessages(response) {
  try{
    response.write(messages.toString());
  }
  catch (ex) {
    console.log(ex)
  }
};

function SendMessage( m,response) {
  if (m.length <= 0) {
    response.write("Сообщение слишком короткое!");
    return;
  }

  messages.unshift(m);
  while (messages.length > MessagesCapacity)
    messages.pop();
  response.write("Сообщение доставлено!");
}