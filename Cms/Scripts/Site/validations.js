$(document).ready(function () {
	
	$.validator.unobtrusive.addValidation = function (selector) {		

		//get the relevant form 
		var form = $(selector);
		// delete validator in case someone called form.validate()
		form.removeData("validator");
		form.removeData("unobtrusiveValidation");
		$.validator.unobtrusive.parse(form);

		var validator = $.data(form.get(0), 'validator').settings;
			validator.errorPlacement = function (error, element) {
				$(element).next(".field-validation-error").attr('data-content', error.get(0).innerText);
			};
			validator.highlight = function (element) {
				$(element).closest(".form-group").addClass("has-error");
			};
			validator.unhighlight = function (element) {
				$(element).closest(".form-group").removeClass("has-error");
			};
			validator.success = function (element,error) {
				$(element).next(".field-validation-error").removeAttr('data-content');
			};
	}

	$.validator.unobtrusive.addValidation($("form").get(-1));
});

$(window).load(function () {
	// Show Model (lightbox)
	$('[data-toggle=modal]').click(function (e) {
		e.preventDefault();
		e.stopPropagation();
		// Check if modal should be large or small

		var modalName = $(this).attr('data-target');
		var smallModal = (modalName.search('Small') != -1);
		$.get(this.href, function (data) {
			$('body').append(data);
			
			$(modalName)	
				.on('hidden.bs.modal', function () { $(this).remove(); })
				.on('show.bs.modal', function () { if (smallModal) $(this).find('.modal-dialog').addClass('modal-sm'); })
				.on('shown.bs.modal', function () { $(this).find('[autofocus]').focus();}) // Set Autofocus to inputfield in modals
				.modal({ show: true, keyboard: true, backdrop: true })
		});
		$(this)
		return false;
	});

	// Set popovers for input validation
	$('[data-toggle=popover]').popover({
 		trigger: 'hover',
 		html: false,
 		placement: 'right',
		container: 'body'
	});
});