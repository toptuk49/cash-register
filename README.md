<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">Cash Register</h3>

  <p align="center">
    🛒 A POS (Point of Sale) module for barcode control, product movement tracking, and stock correction in a retail store.
    <br />
    <br />
    <a href="#about-the-project"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="#usage">Usage</a>
    ·
    <a href="https://github.com/toptuk49/cash-register/issues">Report Bug</a>
    ·
    <a href="https://github.com/toptuk49/cash-register/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>📖 Table of Contents</summary>
  <ol>
    <li><a href="#about-the-project">🏛 About The Project</a></li>
    <li><a href="#features">✨ Features</a></li>
    <li><a href="#built-with">🛠 Built With</a></li>
    <li>
      <a href="#getting-started">🚀 Getting Started</a>
      <ul>
        <li><a href="#prerequisites">📋 Prerequisites</a></li>
        <li><a href="#installation">💾 Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">💡 Usage</a></li>
    <li><a href="#roadmap">🛣 Roadmap</a></li>
    <li><a href="#contributing">🤝 Contributing</a></li>
    <li><a href="#license">📄 License</a></li>
    <li><a href="#contact">✉️ Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## 🏛 About The Project

This repository hosts **“Cash Register”**, a POS (Point of Sale) module designed for retail stores.  
The project is part of a larger **automated system for product accounting and warehouse management**.

The system enables:

- scanning and validating product barcodes,
- registering sales, returns, and write-offs,
- real-time synchronization with warehouse stock,
- reporting and data export to external accounting systems (e.g., 1C).

It is built to minimize cashier errors, speed up operations, and ensure accurate inventory tracking.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- FEATURES -->

## ✨ Features

- 📦 **Product Management**
  - To scan and validate barcodes via barcode scanner
  - To track stock changes in real-time
  - To synchronize with warehouse data

- 💵 **Sales Operations**
  - Sales, returns, and write-offs
  - Automatic stock deduction per transaction
  - Receipt printing and storage

- 📊 **Reporting**
  - Daily / weekly / monthly movement reports
  - Return and write-off reports
  - Discrepancy reports between POS and warehouse

- 🔄 **Integration**
  - Export data to external systems (1C or other accounting software)
  - REST API support for future integrations

- 🛡 **Reliability**
  - Data validation for barcodes
  - Auto-backup of database (at least once a day)
  - Fail-safe recovery after power/network failures

- 🖥 **User Interface**
  - Friendly and simple interface for cashiers
  - Admin panel for stock correction and inventory checks

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- BUILT WITH -->

## 🛠 Built With

- **C# + .NET 8.0.401** — core development platform
- **Entity Framework Core** — planned for database interaction
- **SQL Server / PostgreSQL** — planned database options
- **Windows 10/11, Linux (Ubuntu, Debian)** — supported platforms
- **[mise](https://mise.jdx.dev)** — developer workflow automation

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->

## 🚀 Getting Started

Follow these steps to set up the project locally for development or testing.

### 📋 Prerequisites

You need:

- [mise](https://mise.jdx.dev) (configured for this repo)
- [Git](https://git-scm.com/)
- SQL database (SQL Server or PostgreSQL)
- Barcode scanner (for real hardware testing)

### 💾 Installation

1. Clone the repository:

```sh
git clone https://github.com/toptuk49/cash-register.git
cd cash-register
```

2. Install tools from `mise.toml`:

```sh
mise install
```

3. Run the app (task `run` from mise.toml):

```sh
mise run run
```

4. For hot reload during development:

```sh
mise run watch-run
```

5. Other useful tasks:

```sh
mise run build     # build solution
mise run test      # run tests
mise run format    # format all files
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->

## 💡 Usage

- **Cashier workflow**: Scan product → Check receipt details → Confirm sale → Print receipt

- **Administrator workflow**: Perform stock corrections, generate reports, and run inventory checks

Reports can be exported and integrated into 1C or other accounting systems.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->

## 🛣 Roadmap

- [ ] Implement full reporting module (sales, returns, discrepancies)

- [ ] Add inventory management module

- [ ] Integrate with online fiscal registers

- [ ] REST API for third-party integration

- [ ] Docker container support for deployment

See the [open issues](https://github.com/toptuk49/cash-register/issues) for more.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->

## 🤝 Contributing

Use **conventional commits** (feat:, fix:, docs:, etc.)

1. Fork the Project

2. Create your Feature Branch (git checkout -b feature/<feature-name>)

3. Commit your Changes (git commit -m 'feat: add <feature-name>')

4. Push to the Branch (git push origin feature/<feature-name>)

5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->

## 📄 License

Distributed under the GNU General Public License. See LICENSE for details.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->

## ✉️ Contact

Anton Kuznetsov - [GitHub](https://github.com/toptuk49)
Artem Mikhayelyan -  [GitHub](https://github.com/ffchgvhchh-droid)
Project Link: https://github.com/toptuk49/cash-register

<p align="right">(<a href="#readme-top">back to top</a>)</p>
