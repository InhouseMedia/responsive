$(document).ready(documentReady);
$(window).load(windowLoad);

function documentReady () {
	/* //TODO: Something for later 
	$.extend($.validator.messages, {
		required: "Dieses Feld ist ein Pflichtfeld.",
		email: "Geben Sie bitte eine gültige E-Mail Adresse ein.",
		url: "Geben Sie bitte eine gültige URL ein.",
		date: "Bitte geben Sie ein gültiges Datum ein.",
		number: "Geben Sie bitte eine Nummer ein.",
		digits: "Geben Sie bitte nur Ziffern ein.",
		equalTo: "Bitte denselben Wert wiederholen.",
		range: $.validator.format("Geben Sie bitte einen Wert zwischen {0} und {1} ein."),
		max: $.validator.format("Geben Sie bitte einen Wert kleiner oder gleich {0} ein."),
		min: $.validator.format("Geben Sie bitte einen Wert größer oder gleich {0} ein.")
	});
	*/
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
		container: 'body',
		delay: {'hide': 1000 }
	});
	
	// Change submit button style
	$("form").submit(function (e) {
		var form = $(e.target);

		if (form.find(".has-error").length == 0) {
			// Use parent for standard modal buttons that are outside the form.
			form.offsetParent().find("button[type=submit]").addClass("submit");
		}
	});

	// Trigger standard Save button in modal boxes
	$(".modal button[type=submit]").click(function (item) {
		console.log('click',this,item);
		$(this).offsetParent().find('form').submit();
	});

	console.log("ready");
}

function windowLoad() {

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
				.on('show.bs.modal', function () { if (smallModal) $(this).find('.modal-dialog').addClass('modal-sm');	})
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

	// Setup datetime picker
	var newDate = new Date().getTime() - 3600000;
	$('.input-group.date').datetimepicker({minDate: new Date(newDate)});
	 
	console.log("load");
}