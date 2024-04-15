// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AddSocialProfile() {
    console.log("Here");
    $('#addItemModal').modal('show');
}function editItem(button) {
    var row = $(button).closest('tr');
    var ProfileN = row.find('td:first-child').text();
    var ProfileV = row.find('td:nth-child(2)').text();
    $('#editItemName').val(ProfileN);
    $('#editItemValue').val(ProfileV);
    var itemId = $(button).data('item-id');
    $('#editItemId').val(itemId);
    $('#editItemModal .modal-title').text('Edit Item');
    $('#editItemForm').attr('asp-action', 'Edit');
    $('#editItemModal').modal('show');
}
function EditProfile(button) {
    var row = $(button).closest('tr');
    var profileId = $(button).data('item-id');
    var profileN = row.find('td:first-child').text();
    var profileU = row.find('td:nth-child(2)').text();
    $('#editItemId').val(profileId);
    $('#editItemName').val(profileN);
    $('#editItemValue').val(profileU);
    $('#editItemModal').modal('show');
    }
function deleteItem(button) {
    $(button).closest('tr').remove();
}