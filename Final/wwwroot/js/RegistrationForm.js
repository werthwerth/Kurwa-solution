$("#registerForm").submit(function (event) {
	event.preventDefault
	$('#password').val(sha512($('#password').val()))
});