function ShowLoading() {
    var parentItem = parent;
    while (parentItem.document.getElementById("loadingScript") == undefined) {
        if (parentItem == parentItem.parent) {
            return false;
        }
        else {
            parentItem = parentItem.parent;
        }
    }
    parentItem.ShowLoading();
    return true;
}

function CloseLoading() {
    var parentItem = parent;
    while (parentItem.document.getElementById("loadingScript") == undefined) {
        if (parentItem == parentItem.parent) {
            return false;
        }
        else {
            parentItem = parentItem.parent;
        }
    }
    parentItem.CloseLoading();
    return true;
}