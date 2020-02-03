/*


bsbilal

*/

var express = require('express');
var mysql = require('mysql');
var bodyParser= require('body-parser');


var con=mysql.createConnection({
		host:'localhost',
		user:'root',
		password:'12345678',
		database:'dbcontactssearch'
});

//RESTFul

var app=express();
var publicDir=(__dirname+'/public/');
app.use(express.static(publicDir));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended:true}));

app.get("/person",(req,res,next)=>{
	con.query('SELECT * FROM tblPerson',function(error,result,fields){
			con.on('error',function(err){
				console.log('[MYSQL]ERROR',err);
			});
			if(result&&result.length)
				res.end(JSON.stringify(result));
			else
				res.end(JSON.stringify('No person here'));
			
	});
});

//search as name


app.post("/search",(req,res,next)=>{
	
	var post_data=req.body;
	var name_search=post_data.search; // get search fields

	var query="SELECT * FROM tblPerson WHERE NAME LIKE '%"+name_search+"%'";
	con.query(query,function(error,result,fields){
			con.on('error',function(err){
				console.log('[MYSQL]ERROR',err);
			});
			if(result&&result.length)
				res.end(JSON.stringify(result));
			else
				res.end(JSON.stringify('No person here'));
			
	});
});

app.listen(3000,()=>{
	console.log('Server works at PORT 3000');
});





