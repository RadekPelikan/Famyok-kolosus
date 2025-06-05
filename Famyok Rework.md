## Complete Remaster of [Famyok](https://www.famyok.cz)

It will be separated into:

- Static [Promotional Website](#promotional-website)
- React app ([The Game](#the-game))
- [Admin Panel](#admin-panel)
- [PhpMyAdmin](#phpmyadmin) for DB management
- [Monitoring Panel](#monitoring-panel)
- [Web Developer Environment](#web-developer-environment)

The current data from the MySQL database will be migrated to the new one.  
The design of websites and the app has to be reworked. New designs must be proposed.

### To Discuss:

- [ ] Propose to the community to come up with re-designs
- [ ] Ask if the proposed infrastructure solution sounds good

---

# Famyok Kolosus

## Interface

### Promotional Website

Similar to this layout, but a bit more modern.  
![Promotional Website](.github/imgPasted%20image%2020250224055606.png)

### The Game

Similar to the [Promotional Website](#promotional-website) but distinct enough to be immersive for the player.  
![The Game](.github/imgPasted%20image%2020250224055606.png)

### Admin Panel

New look—boring and simple. Just basic operations: approve, modify, delete, create, view data.

### PhpMyAdmin

Similar to the already deployed MySQL management on Forpsi, but newer.  
![PhpMyAdmin](.github/img2-29.png)

### Monitoring Panel

Displays current errors, bugs, events, changes, login activity, and more.  
**Once a platform is decided, someone will need to create dashboards and monitoring views.**

| Monitoring Tool | Can Run Locally    | Resource Usage   | How to Run Locally |
| --------------- | ------------------ | ---------------- | ------------------ |
| **Loki**        | Yes                | Low to Moderate  | Docker Compose     |
| **Elastic**     | Yes                | Moderate to High | Docker Compose     |
| **Graylog**     | Yes                | Moderate         | Docker Compose     |
| **Splunk**      | Yes (Free Version) | High             | Docker             |

The logging server will likely be provided by **Fluentd**.  
**There are multiple options:**

---

#### Loki

![Loki](.github/imggrafana-2.png)

- **Resource Usage:** Low to moderate.
- **Integration:** Fluentd can send logs to Loki using the **Loki** output plugin.
- **Visualization:** Grafana is used for visualizing logs and metrics.

**Setup Steps:**

1. Fluentd sends logs to Loki over HTTP.
2. Grafana queries Loki for logs and visualizations.

---

#### Elastic

![Elastic](.github/imgblog-elastic-discover-timestamps.png)

- **Resource Usage:** Moderate to high.
- **Integration:** Fluentd can send logs to Elasticsearch using the `elasticsearch` output plugin.
- **Visualization:** Kibana for log exploration and dashboards.

**Setup Steps:**

1. Fluentd sends logs to Elasticsearch over HTTP.
2. Kibana provides real-time visualizations.

---

#### Graylog

![Graylog](.github/imggraylog-histogram.png)

- **Resource Usage:** Moderate. Requires **Elasticsearch** and **MongoDB**.
- **Integration:** Fluentd can forward logs to Graylog via the **GELF** output plugin.
- **Features:** Centralized log management with powerful search and alerting.

**Setup Steps:**

1. Fluentd sends logs to Graylog via GELF.
2. Graylog provides an interface for querying and visualization.

---

#### Splunk

![Splunk](.github/imgim-hero-real-time-header.png)

- **Resource Usage:** High.
- **Integration:** Fluentd can send logs via **HTTP Event Collector (HEC)** or **Splunk Forwarder**.
- **Features:** Advanced search, analysis, and visualization.

**Setup Steps:**

1. Fluentd sends logs to Splunk via HEC.
2. Splunk processes logs for analysis.

---

### Web Developer Environment

Ability to modify service files on the web for quick fixes.

---

## Game

Retain all data, layout, and lore from the current database.

---

## Chat

Utilize one of these:

- **IRC:** [Ergo](https://github.com/ergochat/ergo)
- **WebSockets:** [socket.io](https://github.com/googollee/go-socket.io)

[How to Run Multiple Servers in One Process](https://medium.com/rungo/running-multiple-http-servers-in-go-d15300f4e59f)

---

### IRC vs WebSockets

| Feature                               | WebSockets                    | IRC                                               |
| ------------------------------------- | ----------------------------- | ------------------------------------------------- |
| **Real-Time Communication**           | ✅ Yes (Low latency)          | ✅ Yes (but uses polling)                         |
| **Modern Web Support**                | ✅ Works in browsers natively | ❌ Requires an external IRC client or web gateway |
| **Persistent Connection**             | ✅ Yes                        | ✅ Yes (but may require rejoining channels)       |
| **Custom Features**                   | ✅ Full control               | ⚠️ Limited, depends on the IRC server             |
| **Setup Complexity**                  | ✅ Simple                     | ❌ Complex                                        |
| **Mobile Support**                    | ✅ Yes                        | ❌ Limited                                        |
| **Security (Encryption, Auth, etc.)** | ✅ Supports **SSL/TLS**       | ⚠️ Limited support                                |
| **Offline Messages**                  | ❌ No                         | ⚠️ Requires a bouncer (e.g., ZNC)                 |

---

### **Why WebSockets is the Better Choice**

- ✅ **Native Browser Support**
- ✅ **Low Latency**
- ✅ **Customization:** Custom commands, emojis, dice rolls, etc.
- ✅ **Better Security:** Supports **TLS encryption**
- ✅ **Scalability:** Works with modern backend frameworks.

**Since you’re making a TRPG web app, WebSockets is the best choice.** 🎲💬

---

## New Features

### Optimizations

- image optimizations and compression, correct file format (webp I think) and compressing images: [github](https://github.com/dlemstra/Magick.NET)

### Discord community suggestions

#### Moon cycles

- Visualization of a moon phases on a main page
- Moon moving across the top

##### Moon cycles inside of rooms

- Chats will have feature so storytellers would be able to control it

### Discord Integration

### AI

#### AI Images

#### AI Approval

For character and image uploads.

#### AI Lore

Specialized AI to create and expand the current lore.  
**Can assist the storyteller in creating new scenarios.**
