function RemoveAdmin(name) {
    document.getElementById("text").innerText = `That will make ${name} unauthorized to access any data..\n  Are you sure to remove ${name} from admin?`;
    document.getElementById("addBtn").href = `/account/confirmRemoveAdmin?userName=${name}`;
}