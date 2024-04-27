document.addEventListener("DOMContentLoaded", function () {
    var checkbox = document.getElementById('toggle-password-checkbox');
    var passwordInputs = document.querySelectorAll('.password-input');

    checkbox.addEventListener('change', function () {
        for (var i = 0; i < passwordInputs.length; i++) {
            passwordInputs[i].type = this.checked ? 'text' : 'password';
        }
    });
});
