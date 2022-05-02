<?php

function getBMI($height,$inches,$weight)
{
	$BMI_Value = "";
	$inched_height = ($height*12)+$inches;
	$BMI = ($weight/($inched_height*$inched_height))*703;
	if($BMI < 18.5){
		$BMI_Value = "Underweight";
	}
	else if($BMI > 18.5 && $BMI < 25){
		$BMI_Value = "Normal weight";
	}
	else if($BMI > 24.9 && BMI < 30){
		$BMI_Value = "Overweight";
	}
	else if($BMI > 30){
		$BMI_Value = "Obesity";
	}
    return strval($BMI)." ".$BMI_Value;        
}