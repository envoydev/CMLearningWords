/*
 *
 * For Creationg new word in english Controller: WordInEnglish, Action: Create
 * 
 * */
var createNewWordInEnglishcounter = 0;
//var addButton = createNewWord.addButton;
var addButton = document.getElementById("add-translation-button");

var createNewTranslationCounter = 0;
//var addTranslationButton = createNewWord.addButton;
var addTranslationButton = document.getElementById("add-separate-translation-button");

if (addButton !== 'undefined' && addButton !== null)
{
    //add to button "addButton" action
    addButton.addEventListener("click", addElements);
}

//function for adding element <fieldset> for new objects
function addElements() {
    var dublicated, inputFiled, getButton, spanField;
    var container = document.getElementById("containerOfTranslations");
    var countFiledSets = document.getElementsByClassName("translationFiled").length;
    var lastFiled = document.getElementsByClassName("translationFiled")[countFiledSets - 1];
    //if view page has only single <fieldset>
    if ((countFiledSets - 1) === 0) {
        ublicated = dublicateField(lastFiled);
        clearFiled(dublicated);
        addButtonInputForDeleting(dublicated);
        inputFiled = dublicated.querySelectorAll("[additionalFiled='translationFiledinput']")[0];
        inputFiled.className += " translation-filed";
        changeIdOfInput(inputFiled);
        spanField = dublicated.querySelectorAll("[spanField='validationSpan']")[0];
        addSpanValidation(spanField);
        AddEventForRemove();
    }
    else {
        dublicated = dublicateField(lastFiled);
        dublicated.setAttribute("idForRemove", createNewWordInEnglishcounter);
        inputFiled = dublicated.querySelectorAll("[additionalFiled='translationFiledinput']")[0];
        changeIdOfInput(inputFiled);
        getButton = dublicated.querySelectorAll("[name='removeButton']")[0];
        getButton.setAttribute("idForRemove", createNewWordInEnglishcounter);
        clearFiled(dublicated);
        spanField = dublicated.querySelectorAll("[spanField='validationSpan']")[0];
        addSpanValidation(spanField);
        AddEventForRemove();
    }
    //function for adding delete button for additional fileds
    function addButtonInputForDeleting(element) {
        var button = document.createElement("input");
        button.setAttribute("type", "button");
        button.setAttribute("name", "removeButton");
        button.setAttribute("value", "Удалить текущее поле");
        button.setAttribute("idForRemove", createNewWordInEnglishcounter);
        button.setAttribute("class", "delete-button");
        //element.appendChild(button);
        element.insertBefore(button, element.querySelectorAll("[spanField='validationSpan']")[0]);
    }
    //function for dublicating <fieldset> with inputs
    function dublicateField(element) {
        createNewWordInEnglishcounter++;
        var clone = element.cloneNode(true);
        clone.setAttribute("idForRemove", createNewWordInEnglishcounter);
        return container.appendChild(clone);
    }
    //function for clearing values in duplicating inputs
    function clearFiled(element) {
        var getInputWithValue = element.querySelectorAll("[additionalFiled='translationFiledinput']")[0];
        getInputWithValue.value = "";
    }
    function changeIdOfInput(element) {
        //console.log(element);
        element.setAttribute("id", "Translations_" + createNewWordInEnglishcounter + "__Name");
        element.setAttribute("name", "Translations[" + createNewWordInEnglishcounter + "].Name");
    }
    function addSpanValidation(element) {
        element.setAttribute("data-valmsg-for", "Translations[" + createNewWordInEnglishcounter + "].Name");
    }
}
//Function for remove <fieldset> by current click, which deleting <fieldset idForRemove="value"> with current value of <input idForRemove="value">
function removeElement(e) {
    var thisButton = e.currentTarget;
    var getValue = thisButton.getAttribute("idForRemove");
    if (getValue !== null && getValue !== "undefined") {
        var elem = document.querySelector("[idForRemove='" + getValue + "']");
        var parent = elem.parentNode;
        elem.parentNode.removeChild(elem);
    }
}
//add events for all new remove buttons
function AddEventForRemove() {
    var removeButtons = document.querySelectorAll("[class='delete-button']");
    return removeButtons[removeButtons.length - 1].addEventListener("click", function (e) {
        removeElement(e);
    });
}

/*
 *
 * For Editing word in english Controller: WordInEnglish, Action: Edit
 * Delete Transltion of word in english
 * 
 * */
$(".delete-button").click(function (e) {
    //console.log("fuck");
    e.preventDefault();
    var dataId = $(this).attr('data-id-element');
    var element = $(this).parent();
    var modalFooter = $('.modal-footer');
    var whileDeletingModal = $('#while-deleling');
    var afterDeletingModal = $('#after-deleling');
    var closeButton = $('#closeModal');

    modalFooter.css('display', 'block');
    whileDeletingModal.css('display', 'block');
    afterDeletingModal.css('display', 'none');

    $('#delete-current-translation').click(function (event) {
        event.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/TranslationOfWord/Delete/' + dataId + '',
            success: function () {
                element.remove();
                modalFooter.css('display', 'none');
                whileDeletingModal.css('display', 'none');
                afterDeletingModal.css('display', 'block'); 
            },
            error: function () {
                alert("произошла ошибка во время удаления, попробуйте позже");
            }
        });
        setTimeout('closeButtonFunction()', 2000);

        modalFooter.css('display', 'block');
        whileDeletingModal.css('display', 'block');
        afterDeletingModal.css('display', 'none');
    });
});
function closeButtonFunction() {
    var closeButton = $('#closeModal');
    closeButton.trigger('click');
};

/*
 * 
 * translations_0__Name
 * 
 */

if (addTranslationButton !== 'undefined' && addTranslationButton !== null) {
    //add to button "addButton" action
    addTranslationButton.addEventListener("click", addElementsForTranslationController);
}

//function for adding element <fieldset> for new objects
function addElementsForTranslationController() {
    var dublicated, inputFiled, getButton, spanField, hiddenInput;
    var container = document.getElementById("containerOfTranslations");
    var countFiledSets = document.getElementsByClassName("translationFiled").length;
    var lastFiled = document.getElementsByClassName("translationFiled")[countFiledSets - 1];
    //if view page has only single <fieldset>
    if ((countFiledSets - 1) === 0) {
        dublicated = dublicateTranslationField(lastFiled);
        clearTranslationFiled(dublicated);
        addTranslationButtonInputForDeleting(dublicated);
        inputFiled = dublicated.querySelectorAll("[additionalFiled='translationFiledinput']")[0];
        inputFiled.className += " translation-filed";
        changeIdOfInput(inputFiled);
        hiddenInput = dublicated.querySelectorAll("[additionalFiled='translationFiledinputHidden']")[0];
        changeIdHiddenOfInput(hiddenInput);
        spanField = dublicated.querySelectorAll("[spanField='validationSpan']")[0];
        addSpanValidation(spanField);
        AddEventForTranslationRemove();
    }
    else {
        dublicated = dublicateTranslationField(lastFiled);
        dublicated.setAttribute("idForRemove", createNewTranslationCounter);
        inputFiled = dublicated.querySelectorAll("[additionalFiled='translationFiledinput']")[0];
        changeIdOfInput(inputFiled);
        hiddenInput = dublicated.querySelectorAll("[additionalFiled='translationFiledinputHidden']")[0];
        changeIdHiddenOfInput(hiddenInput);
        getButton = dublicated.querySelectorAll("[name='removeButton']")[0];
        getButton.setAttribute("idForRemove", createNewTranslationCounter);
        clearTranslationFiled(dublicated);
        spanField = dublicated.querySelectorAll("[spanField='validationSpan']")[0];
        addSpanValidation(spanField);
        AddEventForTranslationRemove();
    }
    //function for adding delete button for additional fileds
    function addTranslationButtonInputForDeleting(element) {
        var button = document.createElement("input");
        button.setAttribute("type", "button");
        button.setAttribute("name", "removeButton");
        button.setAttribute("value", "Удалить текущее поле");
        button.setAttribute("idForRemove", createNewTranslationCounter);
        button.setAttribute("class", "delete-button");
        //element.appendChild(button);
        element.insertBefore(button, element.querySelectorAll("[spanField='validationSpan']")[0]);
    }
    //function for dublicating <fieldset> with inputs
    function dublicateTranslationField(element) {
        createNewTranslationCounter++;
        var clone = element.cloneNode(true);
        clone.setAttribute("idForRemove", createNewTranslationCounter);
        return container.appendChild(clone);
    }
    //function for clearing values in duplicating inputs
    function clearTranslationFiled(element) {
        var getInputWithValue = element.querySelectorAll("[additionalFiled='translationFiledinput']")[0];
        getInputWithValue.value = "";
    }
    function changeIdOfInput(element) {
        //console.log(element);
        element.setAttribute("id", "Translations_" + createNewTranslationCounter + "__Name");
        element.setAttribute("name", "Translations[" + createNewTranslationCounter + "].Name");
    }
    function changeIdHiddenOfInput(element) {
        //console.log(element);
        element.setAttribute("id", "Translations_" + createNewTranslationCounter + "__WordInEnglishId");
        element.setAttribute("name", "Translations[" + createNewTranslationCounter + "].WordInEnglishId");
    }
    function addSpanValidation(element) {
        element.setAttribute("data-valmsg-for", "Translations[" + createNewTranslationCounter + "].Name");
    }
}
//Function for remove <fieldset> by current click, which deleting <fieldset idForRemove="value"> with current value of <input idForRemove="value">
function removeTranslationElement(e) {
    var thisButton = e.currentTarget;
    var getValue = thisButton.getAttribute("idForRemove");
    if (getValue !== null && getValue !== "undefined") {
        var elem = document.querySelector("[idForRemove='" + getValue + "']");
        var parent = elem.parentNode;
        elem.parentNode.removeChild(elem);
    }
}
//add events for all new remove buttons
function AddEventForTranslationRemove() {
    var removeButtons = document.querySelectorAll("[class='delete-button']");
    return removeButtons[removeButtons.length - 1].addEventListener("click", function (e) {
        removeTranslationElement(e);
    });
}