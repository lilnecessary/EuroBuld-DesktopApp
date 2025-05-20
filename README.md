# 🏗️ EuroBuld — Магазин строительных услуг

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/lilnecessary/EuroBuld)
[![Platform](https://img.shields.io/badge/platform-Web%20%26%20Desktop-lightgrey)](https://github.com/lilnecessary/EuroBuld)
[![Tech](https://img.shields.io/badge/tech-stack%3A%20Node.js%2C%20C%23%2C%20MSSQL-yellowgreen)](#технологии)

---

## 📌 Описание

**EuroBuld** — это интегрированная система (веб и десктоп-приложение) для продажи строительных услуг. Система позволяет клиентам удобно оформлять заказы, а сотрдникам — управлять всей базой данных в реальном времени.

Проект разделен на две части:
- 🌐 Веб-версия (админ-панель, регистрация, оформление заказов),
- 💻 Десктоп-приложение для сотрудников/менеджеров.

---

## 🔑 Основные функции

### 🔐 Регистрация и Авторизация
Пользователи могут безопасно зарегистрироваться и войти в систему с учетом своей роли:
- **Пользователь** — оформление заказов,
- **Менеджер/Сотрудник** — обработка заявок,
- **Администратор** — полный контроль данных.

### 🛒 Оформление заказа
- Просмотр списка строительных услуг,
- Выбор интересующей услуги,
- Заполнение контактных данных,
- Отправка заявки.

### 🧑‍💼 Панель администратора
- Просмотр и редактирование всех таблиц (Users, Roles, Orders и др.),
- Управление заказами и услугами,
- Поддержка изображений (услуги с фото),
- Проверка данных и валидация.

---

## 📥 Установка и запуск

### 📦 Веб-версия (Node.js + Express)

```bash
# Клонируйте репозиторий
git clone https://github.com/lilnecessary/EuroBuld.git
cd EuroBuld

# Установите зависимости
npm install

# Настройте подключение к MSSQL в .env или config-файле

# Запустите сервер
node index.js
```

---

### 💻 Десктоп-приложение (C# WPF)

📥 Скачать готовый установщик:
👉 [**EuroBuld-DesktopApp**](https://github.com/lilnecessary/EuroBuld-DesktopApp)

> Подключается к той же базе данных, что и веб-версия.

---

## 🖼️ Скриншоты интерфейса

| 📌 Название | 💻 Скриншот |
|------------|-------------|
| **Главная страница** | ![MainPage](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/MainPage.png?raw=true) |
| **Услуги** | ![services](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/ServicePage.png?raw=true) |
| **Страница администратора** | ![AdminPage](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/AdminPage.png?raw=true) |
| **Страница отчетов** | ![ReportPage](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/ReportPage.png?raw=true) |
| **Страница менеджера** | ![ManagerPage](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/ManagerPage.png?raw=true) |
| **Личный кабинет пользователя** | ![PersonalAcountPage](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/PersonalAcountPage.png?raw=true) |
| **Страница авторизации** | ![AuthorizathionPage](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/AuthorizathionPage.png?raw=true) |
| **Страница регистрации** | ![RegistrationPage](https://github.com/lilnecessary/EuroBuld-Website/blob/main/EuroBuld/ScreenShots/RegistrationPage.png?raw=true) |

---

## 🛠️ Технологии

| Компонент        | Технологии                                 |
|------------------|---------------------------------------------|
| Backend          | Node.js, Express, Joi, MSSQL                |
| Frontend         | HTML, CSS, JavaScript                       |
| Desktop          | C# WPF                                      |
| База данных      | Microsoft SQL Server                        |
| Аутентификация   | Сессии + авторизация по ролям               |
| Валидация данных | Joi (сервер), HTML/JS (клиент)              |
| Работа с фото    | base64, хранение изображений в БД           |

---
