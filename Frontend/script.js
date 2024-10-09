document.getElementById('add').addEventListener('click', function() {
    const container = document.getElementById('form-container');
    const companyDiv = document.createElement('div');
    companyDiv.className = 'company';

    companyDiv.innerHTML = `
        <label for="name">Name:</label>
        <input type="text" id="name">
        <br><br>
        <label for="address">Address:</label>
        <input type="text" id="address">
        <br><br>
        <label for="country">Country:</label>
        <input type="text" id="country">
        <button class="add-employee">+1e</button>
        <button class="remove-employee">-1e</button>
        <div class="employees"></div> <!-- Контейнер для сотрудников -->
    `;

    container.appendChild(companyDiv);

    // Добавляем обработчики для новых кнопок сотрудников
    addEmployeeEventListeners(companyDiv);
});

document.getElementById('remove').addEventListener('click', function() {
    const container = document.getElementById('form-container');
    const companies = container.getElementsByClassName('company');
    if (companies.length > 1) {
        container.removeChild(companies[companies.length - 1]);
    }
});

// Функция для добавления обработчиков событий к кнопкам +1e и -1e
function addEmployeeEventListeners(companyDiv) {
    companyDiv.querySelector('.add-employee').addEventListener('click', function() {
        const employeesDiv = companyDiv.querySelector('.employees');
        const employeeDiv = document.createElement('div');
        employeeDiv.className = 'employee';

        employeeDiv.innerHTML = `
            <label for="name_e">Name:</label>
            <input type="text" id="name_e">
            <br><br>
            <label for="age">Age:</label>
            <input type="text" id="age">
            <br><br>
            <label for="position">Position:</label>
            <input type="text" id="position">
        `;

        employeesDiv.appendChild(employeeDiv);
    });

    companyDiv.querySelector('.remove-employee').addEventListener('click', function() {
        const employeesDiv = companyDiv.querySelector('.employees');
        const employeeDivs = employeesDiv.getElementsByClassName('employee');
        if (employeeDivs.length > 0) {
            employeesDiv.removeChild(employeeDivs[employeeDivs.length - 1]);
        }
    });
}

// Кнопка для преобразования в JSON
const convertButton = document.createElement('button');
convertButton.textContent = 'Преобразовать в JSON';
document.body.appendChild(convertButton);

convertButton.addEventListener('click', function() {
    const companies = document.querySelectorAll('.company');
    const result = [];

    companies.forEach(company => {
        const name = company.querySelector('#name').value.trim();
        const address = company.querySelector('#address').value.trim();
        const country = company.querySelector('#country').value.trim();

        // Проверяем, что все поля заполнены
        if (name && address && country) {
            const companyData = {
                name: name,
                address: address,
                country: country,
                employees: []
            };

            const employeesDiv = company.querySelector('.employees');
            const employeeDivs = employeesDiv.getElementsByClassName('employee');

            Array.from(employeeDivs).forEach(emp => {
                const empName = emp.querySelector('.name_e').value.trim();
                const empAge = emp.querySelector('.age').value.trim();
                const empPosition = emp.querySelector('.position').value.trim();

                // Проверяем, что все поля сотрудника заполнены
                if (empName && empAge && empPosition) {
                    companyData.employees.push({
                        name: empName,
                        age: empAge,
                        position: empPosition
                    });
                }
            });

            result.push(companyData);
        }
    });

    // Отображаем результат в виде JSON
    const jsonOutput = JSON.stringify(result, null, 2);

    // Если элемент вывода уже существует, обновляем его, иначе создаем новый
    let outputDiv = document.getElementById('json-output');
    if (!outputDiv) {
        outputDiv = document.createElement('pre');
        outputDiv.id = 'json-output';
        document.body.appendChild(outputDiv);
    }
    outputDiv.textContent = jsonOutput;
});