$(document).ready(articleReady);

function articleReady() {

	$("#collapseActions table.table-drag tbody").draggable();
	/*
	console.log('article ready');
	$("table.table-drag tbody")
		.sortable({
			connectWith: ".connectedSortable",
			items: "> tr:not(:first)",
			appendTo: $tabs,
			helper: "clone",
			zIndex: 999990,
			start: function () { $tabs.addClass("dragging") },
			stop: function () { $tabs.removeClass("dragging") }
		})
		.disableSelection()
	;*/

}