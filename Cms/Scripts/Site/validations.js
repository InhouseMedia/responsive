﻿$(document).ready(function () {
	
	$.validator.unobtrusive.addValidation = function (selector) {		

		//get the relevant form 
		var form = $(selector);
		// delete validator in case someone called form.validate()
		form.removeData("validator");
		form.removeData("unobtrusiveValidation");
		$.validator.unobtrusive.parse(form);

		var validator = $.data(form.get(0), 'validator');
		
		if (validator) {
			validator = validator.settings;
			validator.errorPlacement = function (error, element) {
				$(element).next(".field-validation-error").attr('data-content', error.get(0).innerText);
			};
			validator.highlight = function (element) {
				$(element).closest(".form-group").addClass("has-error");
			};
			validator.unhighlight = function (element) {
				$(element).closest(".form-group").removeClass("has-error");
			};
			validator.success = function (element, error) {
				$(element).next(".field-validation-error").removeAttr('data-content');
			};
		}
	}

	$.validator.unobtrusive.addValidation($("form").last());

	// Set popovers for input validation
	$('[data-toggle=popover]').popover({
		trigger: 'hover',
		html: false,
		placement: 'right',
		container: 'body'
	});
	
	// Change submit button style
	$("form").submit(function (e) {
		var form = $(e.target);

		if (form.find(".has-error").length == 0){
			form.find("button[type=submit]").addClass("submit");
		}
	});
	console.log("ready");
});

$(window).load(function () {
	// Show Model (lightbox)
	$('[data-toggle=modal]').click(function (e) {
		e.preventDefault();
		e.stopPropagation();
		// Check if modal should be large or small
		var btn = $(this);
		var modalName = btn.attr('data-target');
		var smallModal = (modalName.search('Small') != -1);

		// Show loading icon in button
		btn.addClass("submit");

		$.get(this.href, function (data) {
			$('body').append(data);
			btn.removeClass('submit');
			$(modalName)	
				.on('hidden.bs.modal', function () { $(this).remove(); })
				.on('show.bs.modal', function () { if (smallModal) $(this).find('.modal-dialog').addClass('modal-sm'); })
				.on('shown.bs.modal', function () { $(this).find('[autofocus]').focus(); }) // Set Autofocus to inputfield in modals 
				.modal({ show: true, keyboard: true, backdrop: true })
		});
		return false;
	});


	// Show tooltips
	$('[data-toggle="tooltip"]').tooltip(
		{
			'trigger' : 'hover',
			'animation': true,
			'delay': {'show':500, 'hide': 1000}
		}
	);
	

	console.log("load");
});