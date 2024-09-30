() => {
    class Currency {
        constructor(name, code, currentRate, date) {
            this.Name = name;
            this.Code = code;
            this.ExchangeRate = currentRate;
            this.ValueDate = date;
        }
    };
    var initialList = document.getElementById('titulo1').parentNode.children;
    var date;
    var actualList = [];
    const dataList = [];
    for (let currentElement of initialList) {
        if (currentElement.id !== '' && currentElement.id !== 'titulo1') {
            actualList.push(currentElement);
        }
        if (!date && currentElement.className === "pull-right dinpro center") {
            date = currentElement.querySelector('span').getAttribute('content');
        }

    }
    for (let currentElement of actualList) {
        dataList.push(new Currency(
            currentElement.id,
            currentElement.firstElementChild.firstElementChild.querySelector('span').innerHTML.trim(),
            currentElement.firstElementChild.firstElementChild.querySelector('strong').innerHTML.trim().replace(',', '.'),
            date
        ))
    }
    return dataList;
}
