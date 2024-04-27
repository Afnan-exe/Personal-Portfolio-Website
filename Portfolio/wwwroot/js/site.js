jQuery.noConflict();
$(document).ready(function () {
    var holdTimer; // Variable to hold the timer

    // Function to start the timer when link is clicked
    $('#redirectLink').on('mousedown', function () {
        holdTimer = setTimeout(function () {
            window.location.href = '/LoginRegisters/ULogin'; // Redirect to the specified controller action
        }, 2000); // 2000 milliseconds = 2 seconds
    });

    // Function to clear the timer when link is released before 3 seconds
    $('#redirectLink').on('mouseup mouseleave', function () {
        clearTimeout(holdTimer);
    });
});

