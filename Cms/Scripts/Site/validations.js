$.validator.setDefaults({
	highlight: function (element) {
		var e = $(element);
		e.closest(".form-group").addClass("has-error");
		e.next(".text-danger").addClass("glyphicon glyphicon-remove form-control-feedback");
	},
	unhighlight: function (element) {
		var e = $(element);
		e.closest(".form-group").removeClass("has-error");
		e.next(".text-danger").removeClass("glyphicon glyphicon-remove form-control-feedback");
	}
});