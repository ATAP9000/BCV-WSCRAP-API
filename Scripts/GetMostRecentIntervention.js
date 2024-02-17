() => {
    class Intervention {
        constructor(InterventionNumber, ExchangeRate, InterventionDate) {
            this.interventionNumber = InterventionNumber;
            this.exchangeRate = ExchangeRate;
            this.interventionDate = InterventionDate;
        }
    };

    var firstElement = document.getElementsByClassName("odd letra-pequeña views-row-first");

    var result = new Intervention(
        firstElement[0].querySelector('.views-field-field-nro-de-intervencion').innerText.trim(),
        firstElement[0].querySelector('.views-field-field-monto-intervencion').innerText.trim().replace(',', '.'),
        firstElement[0].querySelector('.date-display-single').getAttribute('content').trim()
    );

    return result;
}
