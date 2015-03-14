﻿$(document).ready(articleReady);

function articleReady() {
	var draggable = $("table.table-drag tbody tr").draggable(
		{
			addClasses: false,
			distance: 10,
			helper: function (item) {
				var text = $(this).text().trim().capitalize();
				return getHelper(text);
			},
			containment: '#content',
			connectWith: '#content',
			connectToSortable: '#content',
			stop: function (event, ui) {
				ui.helper.addClass('loading');

				// Get render action that's been dragged into the content holder
				$.get('/Shared/EditorTemplates/Article_Content_' + ui.helper.text(), function (item) {
					this.helper.after($(item));
					this.helper.remove();
				}.bind(ui)).error(function (item) {
					this.helper.removeClass('loading').css({ 'opacity': 0.5 })
				}.bind(ui));
			}
		}
	).disableSelection();

	var widget = draggable.data('ui-draggable');

	draggable.dblclick(function(e){
			widget.trigger('mousedown');
		}
	);

	$("#content").sortable(
		{
			addClasses: false,
			distance: 10,
			axis: 'y',
			containment: '#content',
			connectWith: '#content',
			receive: function (event, ui) {
				ui.helper.first().removeAttr('style').removeClass('.ui-sortable-helper'); // undo styling set by jqueryUI
			},

			helper: function (event, ui) {
				var text = $(ui).find('.panel-heading').text();
				return getHelper(text);
			},
			
			start: function (event, ui) {
				//todo: can't dragg a collapsed content box directly downwards
				var panels = $(this).find('.collapse.in');

				var heightList = $(this).find('.panel').map(function (key, item) { return $(item).outerHeight(); });
					heightList.push(40); // needed when you want to sort when all panels are open.(specify a default small height)
				var height = Math.min.apply(Math, heightList);
				
				$(this).find('.ui-sortable-placeholder').css({ 'height': height + 'px' });			
			},
			stop: function (event, ui) {
				var panels = $(this).find('.tempCollapse .collapse');
				panels.collapse('show');
				panels.closest('.tempCollapse').removeClass('tempCollapse');
				
				// is used when a new article is created and the content droppable holder is empty 
				$(this).removeClass('empty');
			}

		}
	).droppable(
		{
			activeClass: 'highlight'
		}
	).disableSelection();
}

function getHelper(text) {
	var head = $('<h4>', { 'class': 'panel-title' }).append(text);
	var header = $('<div>', { 'class': 'panel-heading' }).append(head);
	var panel = $('<div>', { 'class': 'panel panel-default ui-sortable-helper tempCollapse' }).append(header);
	return panel;
}






// Change the save bar on success
function onSuccess(ajaxContext) {
	progressBar.attr("title", "");
	progressBar.removeClass("progress-bar-striped")
		.removeClass("active")
		.removeClass("loading")
		.addClass("loaded");
	setTimeout(function () { progressBar.removeClass("loaded") }, 300);
}

// Change the save bar into an error bar showing a tooltip with the error in it
function onFailure(ajaxContext) {
	var errorMessage = $(ajaxContext.responseJSON).get(0).ErrorMessage;

	tooltip.tooltip('enable');
	tooltip.attr("title", errorMessage)
		.tooltip('fixTitle');

	progressBar.removeClass("progress-bar-striped")
		.removeClass("active")
		.removeClass("loading")
		.addClass("loaded")
		.addClass("progress-bar-danger");
}
