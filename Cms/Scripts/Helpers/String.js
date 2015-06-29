define(function () {
	String.prototype.isNullOrEmpty = function () {
		// 
	};

	//Capitalize first letter of a String
	String.prototype.capitalize = function () {
		//return this.replace(/^[a-z]/, function(m){ return m.toUpperCase() });
		return this[0].toUpperCase() + this.substring(1);
	};
});