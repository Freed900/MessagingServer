const express = require("express");
const app = express();

const hostname = '127.0.0.1';
const port = 3000;

var messages = [];
var MessagesCapacity=5;

app.get("/getMessages", function(request, response){
  response.send(messages);
});

app.get("/sendMessage", function(request, response){
  var m =request.query.message;
  
  if(m.length<=0) {
    response.send("Сообщение слишком короткое!");
    return;
  }

  messages.unshift(m);
  while(messages.length>MessagesCapacity)
    messages.pop();

  response.send("Сообщение доставлено!");
});

app.listen(port,hostname,()=>{
  console.log("Server started on "+port);
});
