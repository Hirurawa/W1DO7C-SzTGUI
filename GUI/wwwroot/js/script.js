let cars = []
let brands = []
let connection = null;

let carIDtoupdate = -1;

getdata();
setupSignalR();


function setupSignalR() {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:24577/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("CarCreated", (user, message) => {
    getdata();
  });

  connection.on("CartDeleted", (user, message) => {
    getdata();
  });

  connection.on("CarUpdated", (user, message) => {
    getdata();
  });

  connection.onclose(async () => {
    await start();
  });
  start();
}

async function start() {

  try {
    await connection.start();
    console.log("SignalR Connected.");
  } catch (err) {
    console.log(err);
    setTimeout(start, 5000);
  }
}

async function getdata() {
  await fetch('http://localhost:24577/api/Car/GetAll')
    .then(x => x.json())
    .then(y => {
      cars = y;
    });

  await fetch('http://localhost:24577/api/Car/GetAllBrands')
    .then(x => x.json())
    .then(y => {
      brands = y;
      display();
    });
}

function display() {

  document.getElementById('cararea').innerHTML = "";
  cars.forEach(c => {
    document.getElementById('cararea').innerHTML +=
      "<tr><td>" + c.id + "</td><td>" + brands.find(b => b['id'] == parseInt(c.brandId))['name'] + "</td><td>"
      + c.model + "</td><td>" + c.price + "</td><td>"
      + `<button type="button" onclick="removecar(${c.id})">Delete</button>`
      + `<button type="button" onclick="showcarupdate(${c.id})">Update</button>`
      + "</td ></tr>";
  });

  document.getElementById('selectbrand').innerHTML = "";
  brands.forEach(b => {
    document.getElementById('selectbrand').innerHTML +=
      "<option value=" + b.id + ">" + b.name + "</option>";
  });
  document.getElementById('selectbrandupdate').innerHTML = "";
  brands.forEach(b => {
    document.getElementById('selectbrandupdate').innerHTML +=
      "<option value=" + b.id + ">" + b.name + "</option>";
  });

  document.getElementById('brandarea').innerHTML = "";
  brands.forEach(b => {
    document.getElementById('brandarea').innerHTML +=
      "<tr><td>" + b.id + "</td><td>" + b.name + "</td><td>"
      + "</td ></tr>";
  });
}

function createcar() {
  let brand = parseInt(document.getElementById('selectbrand').value);
  let model = document.getElementById('model').value;
  let price = parseInt(document.getElementById('price').value);

  fetch('http://localhost:24577/api/Car/Create/', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json', },
    body: JSON.stringify(
      {
        brandId: brand,
        model: model,
        price: price
      }),
  })
    .then(response => response)
    .then(data => {
      console.log('Success:', data);
      getdata();
    })
    .catch((error) => {
      console.error('Error:', error);
    });

}

function showcarupdate(id) {
  document.getElementById('selectbrandupdate').value = cars.find(t => t['id'] == id)['brandId'];
  document.getElementById('updatemodel').value = cars.find(t => t['id'] == id)['model'];
  document.getElementById('updateprice').value = cars.find(t => t['id'] == id)['price'];
  document.getElementById('updatecardiv').style.display = 'flex';
  //document.getElementById('cardiv').style.display = 'none';
  carIDtoupdate = id;
}

function updatecar() {
  document.getElementById('updatecardiv').style.display = 'none';
  //document.getElementById('cardiv').style.display = 'flex';

  let brandId = parseInt(document.getElementById('selectbrandupdate').value);
  let model = document.getElementById('updatemodel').value;
  let price = parseInt(document.getElementById('updateprice').value);

  fetch('http://localhost:24577/api/Car/Update/', {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json', },
    body: JSON.stringify(
      {
        carID: carIDtoupdate,
        brandId: brandId,
        model: model,
        price: price
      }),
  })
    .then(response => response)
    .then(data => {
      console.log('Success:', data);
      getdata();
    })
    .catch((error) => {
      console.error('Error:', error);
    });
}

function removecar(id) {
  let response = confirm("Are you sure?");

  if (response) {
    fetch('http://localhost:24577/api/Car/Delete/' + id, {
      method: 'DELETE',
      headers: { 'Content-Type': 'application/json', },
      body: null
    })
      .then(response => response)
      .then(data => {
        console.log('Success:', data);
        getdata();
      })
      .catch((error) => { console.error('Error:', error); });
  }
}