$(document).ready(documentReady);

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
	
			// Add default Publish date to inputfield when empty
			var pubdate = form.find('[id*=Published_Date]');
			if (pubdate != null && pubdate.val() == '') {
				//pubdate.val(pubdate.attr('placeholder'));
				pubdate.val('1-1-0001 00:00:00');
			}
		}
	});

	// Trigger standard Save button in modal boxes
	$(".modal button[type=submit]").click(function (item) {
		$(this).offsetParent().find('form').submit();
	});
}