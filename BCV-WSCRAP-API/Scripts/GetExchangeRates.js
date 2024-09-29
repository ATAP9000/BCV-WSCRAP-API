() => {
    const formatDate = (date) => {
        const day = date.getDate();
        const month = date.toLocaleString('default', { month: 'short' });
        const year = date.getFullYear();
        return `${day} ${month} ${year}`;
    };

    var cat = localStorage.getItem("partialValue");
    if (cat !== null) {
        localStorage.removeItem("partialValue");
        var htmlTable = document.getElementsByClassName("views-field-field-fecha-del-indicador")[0].parentNode.parentNode.parentNode.innerHTML;
        return htmlTable;
    }

    var minDate = new Date();
    var maxDate = new Date();;

    if (true)
        minDate.setDate(minDate.getDate() - 6);

    document.getElementById("edit-field-fecha-del-indicador-value-min-datepicker-popup-0").value = formatDate(minDate);
    document.getElementById("edit-field-fecha-del-indicador-value-max-datepicker-popup-0").value = formatDate(maxDate);

    var form = document.forms["views-exposed-form-nuevo-indicador-page-1"];
    let formData = new FormData(form);
    let search = new URLSearchParams(formData);
    let queryString = search.toString();
    localStorage.setItem("partialValue", "hellothere");
    return queryString;
}
