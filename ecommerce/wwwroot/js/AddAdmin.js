function MakeAdmin(name) {
    document.getElementById("text").innerText = `That will make ${name} authorized to access any data..\n  Are you sure to make ${name} an admin?`;
    document.getElementById("addBtn").href = `/account/confirmMakeAdmin?userName=${name}`;
}