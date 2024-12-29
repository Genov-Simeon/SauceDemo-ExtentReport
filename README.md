# ENDAVA

# Environment Variables Documentation

This document explains how to configure environment variables for the SauceDemo automation project. These variables are used to manage user credentials securely and are essential for running the tests.

---

## **Environment Variables**

You need to set the following environment variables in your system to run the project:

### **User Credentials**

| Variable Name             | Description                              | Example Value             |
|---------------------------|------------------------------------------|---------------------------|
| `USER_STANDARD`           | Username for Standard User.              | `standard_user`           |

#### **Shared Password**

| Variable Name    | Description                      | Example Value    |
|------------------|----------------------------------|------------------|
| `PASSWORD`       | Password for all user accounts. | `secret_sauce`   |

---

## **Setting Environment Variables**

### **1. Windows**

#### Using Command Prompt:
1. Open Command Prompt with Administrator privileges.
2. Run the following commands:
   ```bash
   setx USER_STANDARD "standard_user" /M
   setx PASSWORD "secret_sauce" /M
