$(document).ready(articleReady);

function articleReady() {
	console.log('article ready');
	
	$("table.table-drag tbody tr").draggable(
		{
			addClasses: false,
			distance: 10,
			helper: function (item) {
				var text = $(this).text();
				var head = $('<h4>', { 'class': 'panel-title' }).append(text.trim().capitalize());
				var header = $('<div>', { 'class': 'panel-heading' }).append(head);
				var panel = $('<div>', { 'class': 'panel panel-default ui-sortable-helper'}).append(header);
				return panel;
			},
			containment: '#content',
			connectWith: '#content',
			connectToSortable: '#content'
		}
	).disableSelection();

	$("#content").sortable(
		{
			distance: 10,
			helper: 'clone',
			containment: '#content',
			connectWith: '#content',
			receive: function (event, ui) {
				ui.helper.first().removeAttr('style').removeClass('.ui-sortable-helper'); // undo styling set by jqueryUI
			}/*,
			activate: function (event, ui) { console.log('activate', event, ui) },
			change: function(event, ui){console.log('change', event, ui)},
			start: function (event, ui) {
				console.log('start', event, ui);
				//console.log(ui.helper.first())
				var collapse = ui.helper.find('.in');
				collapse.setStyle({ 'border': '1px solid red' });
				//console.log(collapse);
				if (collapse) {
					//collapse.toggleClass('collapsing');
				}
			}*/

		}
	).droppable(
		{
			activeClass:'highlight'
		}
	).disableSelection();

	/*
	$("table.table-drag tbody tr")
		.draggable({
			helper: 'clone',
			revert: true,
			connectWith: ".col-md-8",
			appendTo: '.col-md-8',
			containment: '.row'
		});


	$("#content").sortable({
		connectWith: "#content",
		//items: "> tr:not(:first)",
		appendTo: '.col-md-8',
		containment: '.col-md-8',
		//helper: 'clone',

		zIndex: 999990//,
		//start: function () { $tabs.addClass("dragging") },
		//stop: function () { $tabs.removeClass("dragging") }
	})
		.disableSelection()
		.droppable(
		{
			accept: function (d) {
				// Accept only the components we want  
				if (d.hasClass("ui-draggable")) {
					return true;
				}
			},

			drop: function (event, ui) {
				// Create similar shape as the original	
				//debugger;
				$droppedShape = ui.helper.clone();
				$container = $('#content');
				$droppedShape.appendTo($container);
				$droppedShape.show();

				// Remove the helper
				ui.helper.remove();
			}
		}

		);
	*/
	/*
	$(".col-md-8").droppable({
		accept: "table.table-drag tbody tr",
		hoverClass: "ui-state-hover",
		over: function (event, ui) {
			var $item = $(this);
			$item.find("a").tab("show");
		},
		drop: function (event, ui) {
			return false;
		}
	});*/
}