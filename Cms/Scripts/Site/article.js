$(document).ready(articleReady);

function articleReady() {
	console.log('article ready');
	
	$("table.table-drag tbody")
		.sortable({
			//connectWith: ".col-md-8",
			//items: "> tr:not(:first)",
			appendTo: '.col-md-8',
			containment: '.col-md-8',
			//helper: "clone",
			zIndex: 999990//,
			//start: function () { $tabs.addClass("dragging") },
			//stop: function () { $tabs.removeClass("dragging") }
		})
		.disableSelection();


	$(".col-md-8").droppable({
		accept: "table.table-drag tbody tr",
		hoverClass: "ui-state-hover",
		/*over: function (event, ui) {
			var $item = $(this);
			$item.find("a").tab("show");

		},*/
		drop: function (event, ui) {
			return false;
		}
	});
}