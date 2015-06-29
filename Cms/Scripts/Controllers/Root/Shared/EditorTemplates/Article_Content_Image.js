define(['jquery', 'bootstrap', 'bootstrap-slider', 'form2js'], function ($) {
	"use strict";
	var imageChangeTimeout = 0;
	var panel;
	var maxFilesize;
	var dictDefaultMessage;
	
	function _loadDropZone() {

		panel.find('.dropzone').dropzone(
			{
				url: '/File/SaveImage',
				maxFilesize: maxFilesize,
				maxFiles: 1,
				acceptedFiles: 'image/*',
				dictDefaultMessage: dictDefaultMessage
			}
		);
	}

	function _bootstrapSlider() {
		panel.find('.slider').bootstrapSlider();
	}

	function _triggerImageChange(){
		panel.on('change', 'input[name*=imageConfig]', _executeImageChangeTimeout);
	}

	function _executeImageChangeTimeout() {
		window.clearTimeout(imageChangeTimeout);
		imageChangeTimeout = window.setTimeout(_executeImageChange.bind(panel), 350);
	}

	function _executeImageChange(e){
		var test = form2js(
			{
				rootNode: this.get(0),
				skipEmpty: true,
				useIdIfEmptyName: false
			}
		);

		test.imageConfig.custom = true;

		var repl = new RegExp('\\_', 'gm');
		var urlQuery = $.param(test.imageConfig).replace(repl, '.');

		console.info(urlQuery); 

		var img = this.find('img').get(0);
			img.src = img.src.split("?")[0] +"?"+ urlQuery;
	}

	return {
		start: function () {
			_loadDropZone();
			_bootstrapSlider();
			_triggerImageChange();
		},
		model: function (modelId) { panel = $('#collapseImage_' + modelId); },
		message: function (text) { dictDefaultMessage = text; },
		maxfilesize: function (size) { maxFilesize = size; }
		
	}
});
