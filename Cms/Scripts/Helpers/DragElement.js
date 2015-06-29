define(["jquery"],function ($) {
	function _getHelper(text) {
		var head = $('<h4>', { 'class': 'panel-title' }).append(text);
		var header = $('<div>', { 'class': 'panel-heading' }).append(head);
		var panel = $('<div>', { 'class': 'panel panel-default ui-sortable-helper tempCollapse' }).append(header);
		return panel;
	}

	return _getHelper;
});