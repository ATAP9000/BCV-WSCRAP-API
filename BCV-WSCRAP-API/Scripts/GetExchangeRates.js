() => {
    var cat = localStorage.getItem("partialValue");
    if (cat !== null) {
        localStorage.removeItem("partialValue");
        var ea = document.getElementsByClassName("views-field-field-fecha-del-indicador")[0].parentNode.parentNode.parentNode.innerHTML;
        return ea;
    }

    jQuery("#edit-field-fecha-del-indicador-value-min-datepicker-popup-0").click();
    jQuery("#edit-field-fecha-del-indicador-value-min-datepicker-popup-0").focus();
    jQuery("#edit-field-fecha-del-indicador-value-max-datepicker-popup-0").click();
    jQuery("#edit-field-fecha-del-indicador-value-max-datepicker-popup-0").focus();
    jQuery("#edit-field-fecha-del-indicador-value-min-datepicker-popup-0").datepicker("setDate", 'today');
    jQuery("#edit-field-fecha-del-indicador-value-max-datepicker-popup-0").datepicker("setDate", new Date());

    var form = document.forms["views-exposed-form-nuevo-indicador-page-1"];
    let formData = new FormData(form);
    let search = new URLSearchParams(formData);
    let queryString = search.toString();
    localStorage.setItem("partialValue", "hellothere");
    return queryString;
}
