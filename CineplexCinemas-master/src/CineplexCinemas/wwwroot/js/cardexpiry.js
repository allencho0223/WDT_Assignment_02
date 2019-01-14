function expiryChecker() {
	
	// Get current month and year
	var date = new Date();
	var crntMonth = date.getMonth() + 1;
	var crntYear = date.getFullYear() - 2000;
	if (crntMonth.length < 2) { crntMonth = "0" + crntMonth; }
	
	// Get month and year values from the user
	var expiryYear = document.getElementById("expiryYear").value;
	var expiryMonth = document.getElementById("expiryMonth").value;
	// if current year is greater than expiry year, don't submit
	if (crntYear > expiryYear)
	{
	    alert("Your credit card is already expired.");
		return false;
	}
	// if expiry year is equal to current year and expiry month is less than current month or greater than 12, don't submit
	else if (crntYear <= expiryYear && (expiryMonth < crntMonth))
	{
	    alert("Your credit card is already expired.");
		return false;
	}
    else if(expiryMonth > 12)
    {
        alert("Invalid Month is entered.");
        return false;
    }
	// If valid, submit
	else
	{
		return true;
	}
}