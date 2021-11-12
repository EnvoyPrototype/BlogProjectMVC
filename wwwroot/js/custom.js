let index = 0;

function AddTag() {
    // Reference to TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    // Use search function to detect errors
    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        // Trigger Sweet Alert for error condition (searchResult variable)
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });

    } else {
        // Create new select option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    // Clear TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) {
        return false;
    }

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: '<span class="font-weight-bolder">CHOOSE A TAG BEFORE DELETING</span>'
        });
        return true;
    }

    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else
            tagCount = 0;
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

// Look for tagValues variable to see if it has data
if (tagValues != "") {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        // Load or replace current options
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

// Search function detects empty or duplicate tags on current post only
// Returns error string if error detected
function search(str) {
    if (str == "") {
        return "Please don't use an empty tag";
    }

    var tagsEl = document.getElementById("TagList");
    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `#${str} is a duplicate tag - try another!`;
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
    },
    imageUrl: '/img/oops.webp',
    timer: 3000,
    buttonsStyling: false
});