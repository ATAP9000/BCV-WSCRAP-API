() => {
    class Currency {
        constructor(name, code, currentRate) {
            this.Name = name;
            this.Code = code;
            this.CurrentRate = currentRate;
        }
    };
    var initialList = document.getElementById('titulo1').parentNode.children;
    var actualList = [];
    const dataList = [];
    for (let currentElement of initialList) {
        if (currentElement.id !== '' && currentElement.id !== 'titulo1') {
            actualList.push(currentElement);
        }
    }
    for (let currentElement of actualList) {
        dataList.push(new Currency(
            currentElement.id,
            currentElement.firstElementChild.firstElementChild.querySelector('span').innerHTML.trim(),
            currentElement.firstElementChild.firstElementChild.querySelector('strong').innerHTML.trim().replace(',', '.')
        ))
    }
    return dataList;
}
