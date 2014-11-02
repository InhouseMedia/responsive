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

$(window).load(function () {
	//Show Model (lightbox)
	$('[data-toggle=modal]').click(function (e) {
		e.preventDefault();
		$.get(this.href, function (data) {
			$(data).modal({ show: true, keyboard: true, modal: true, backdrop: true });
		});
		return false;
	});
});