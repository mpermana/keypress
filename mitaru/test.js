user = {id:0,name:"michael"};
//user = "mpermana";
if ("object" == typeof user) {
	WSH.echo(JSON);
} else {
	WSH.echo(user);
}