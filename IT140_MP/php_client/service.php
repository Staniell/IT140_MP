<?php
require 'nusoap/lib/nusoap.php';
require 'fetchdata.php';

$server = new nusoap_server(); // Create a instance for soap server

$server->configureWSDL("Soap Service Test","urn:soaptest"); // Configure Web Services Description Language file

$server->register(
	"getBMI", // name of function or method
	array("height"=>"xsd:integer","inches"=>"xsd:integer","weight"=>"xsd:integer"),  // inputs
	array("return"=>"xsd:string")  // outputs
);

$server->service(file_get_contents("php://input"));

?>