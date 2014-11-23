var timeoutObj;
var progressBar = $(".navbar .progress .progress-bar");
var tooltip = progressBar.parent();

$("#userForm input[type=checkbox]").change(
	function (item) {
		clearTimeout(timeoutObj);
		progressBar.removeClass("countDown")
			.removeClass("loaded")
			.removeClass("progress-bar-danger");
		timeoutObj = setTimeout(timeout, 4000);
		setTimeout(function () {
			progressBar.addClass("countDown")
			.addClass("progress-bar-info")
		}, 0);
	}
);

function timeout() {
	progressBar.removeClass("countDown")
		.addClass("progress-bar-striped")
		.addClass("active")
		.addClass("loading");
	$('#userForm').submit();
}

tooltip.on('hidden.bs.tooltip', function (e) {
	console.log('hidden');
	progressBar.removeClass("loaded");
	$(this).tooltip('disable');
	setTimeout(function () {
		progressBar.removeClass("progress-bar-danger");
	}, 1500);
});

function onSuccess(ajaxContext) {
	progressBar.attr("title", "");
	progressBar.removeClass("progress-bar-striped")
		.removeClass("active")
		.removeClass("loading")
		.addClass("loaded");
	setTimeout(function () { progressBar.removeClass("loaded") }, 300);
}

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

