define(["locale", "jquery", "bootstrap", "datetimepicker", "moment"], function (Locale, $) {
	"use strict";

	// Setup datetime picker
	var newDate = new Date().getTime() - 3600000;
	$('.input-group.date').datetimepicker(
		{
			minDate: new Date(newDate),
			widgetParent: 'body',
			language: (typeof Locale == 'string') ? Locale : 'en-US'
		}
	);
});


