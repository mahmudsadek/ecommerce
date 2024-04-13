function MakeAdmin(name) {
    document.getElementById("text").innerText = `That will make ${name} authorized to access any data..\n  Are you sure to make ${name} an admin?`;
    document.getElementById("confirmBtn").href = `/account/confirmMakeAdmin?userName=${name}`;
}

function deleteAccount(name) {
    document.getElementById("text").innerText = `Are you sure to delete ${name}'s account?`;
    document.getElementById("confirmBtn").href = `/Dashbourd/deleteAccount?userName=${name}`;
}
