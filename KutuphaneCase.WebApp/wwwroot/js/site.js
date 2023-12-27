// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#return-date').datetimepicker({
    "allowInputToggle": true,
    "showClose": true,
    "showClear": true,
    "showTodayButton": true,
    "format": "DD/MM/YYYY",
});

var bookBorrowModal = document.getElementById('bookBorrowModal')
bookBorrowModal.addEventListener('show.bs.modal', function (event) {
    // Button that triggered the modal
    var button = event.relatedTarget
    // Extract info from data-bs-* attributes
    var bookidValue = button.getAttribute('data-bs-bookid')
    var bookIdHidden = bookBorrowModal.querySelector('#bookId')
    bookIdHidden.value = bookidValue
})