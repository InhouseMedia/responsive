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
			addClasses: false,
			containment: '#content',
			connectWith: '#content',
			receive: function (event, ui) {
				ui.helper.first().removeAttr('style').removeClass('.ui-sortable-helper'); // undo styling set by jqueryUI
			},
			start: function (event, ui) {
				//todo: can't dragg a collapsed content box directly downwards
				var panels = $(this).find('.collapse.in');

				var heightList = $(this).find('.panel').map(function (key, item) { return $(item).outerHeight(); });
				heightList.push(45); // needed when you want to sort when all panels are open
				var height = Math.min.apply(Math, heightList);

				$(this).find('.ui-sortable-placeholder').css({ 'height':height + 'px' });
			
				panels.closest('.panel')
					.addClass('tempCollapse')
					.css({ 'height': 'auto' });

				panels.collapse('hide');				
			},
			stop: function (event, ui) {

				//console.log(event, ui, this);
				var panels = $(this).find('.tempCollapse .collapse');
				panels.collapse('show');
				panels.closest('.tempCollapse').removeClass('tempCollapse');
			}

		}
	).droppable(
		{
			activeClass: 'highlight'
		}
	).disableSelection();

}