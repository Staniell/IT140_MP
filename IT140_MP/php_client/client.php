<?php
require 'nusoap/lib/nusoap.php';

$client = new nusoap_client("http://localhost/IT140P/service.php?wsdl"); // Create a instance for nusoap client

?>
<!DOCTYPE html>
<html lang="en">
<head>
  <title>SOAP Web Service Client Side Demo</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>

<div class="container">
  <h2>SOAP Web Service Client Side</h2>
  <form class="form-inline" action="" method="POST">
    <div class="form-group">
    <table class="table table-striped table-dark">
  <thead>
    <tr>
      <td colspan="4" class="text-center"><h2>BMI Calculator</h2></td>
    </tr>
    <tr>
      <th scope="col" class="text-center">Height (feet)</th>
      <th scope="col" class="text-center">Inches</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><input type="number" name="height" class="form-control-sm"/></td>
      <td><input type="number" name="inches" class="form-control-sm"/></td>
    </tr>
    <tr>
      <td colspan="4" class="text-center"><strong>Weight (pounds)</strong></td>
    </tr>
    <tr>
      <td colspan="4" class="text-center"><input type="number" name="weight" class="form-control-sm"/></td>
    </tr>
    <tr>
      <td class="text-center" colspan="4"><input type="submit" name="submit" class="btn btn-default"></input></td>
    </tr>
    <tr>
      <td class="text-center" colspan="4">
<strong>
        <?php
  	if(isset($_POST['submit']))
  	{
  		$height =  $_POST['height'];
      $inches =  $_POST['inches'];
  		$weight =  $_POST['weight'];
  
  		$response = $client->call('getBMI',array("height"=>$height,"inches"=>$inches,"weight"=>$weight));
  
  		if(empty($response))
  			echo "No data to extract from the SOAP Response";
  		else
  			echo $response;}?>
</strong>
      </td>
    </tr>
  </tbody>
</table>

      
    </div>
  </form>
  <h3>

  <?php
	if(isset($_POST['submit']))
	{
          echo "<h2>Request</h2>";
	  echo "<pre>" . htmlspecialchars($client->request, ENT_QUOTES) . "</pre>";
          echo "<h2>Response</h2>";
          echo "<pre>" . htmlspecialchars($client->response, ENT_QUOTES) . "</pre>";
	}
   ?>

  </h3>
</div>
</body>
</html>