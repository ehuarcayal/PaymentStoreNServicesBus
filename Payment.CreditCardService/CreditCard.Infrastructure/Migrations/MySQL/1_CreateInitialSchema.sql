CREATE TABLE outbox(
  outbox_id BIGINT(20) NOT NULL AUTO_INCREMENT,
  message_id VARCHAR(255) NOT NULL,
  dispatched TINYINT(1) NOT NULL,
  dispatched_at DATETIME DEFAULT NULL,
  transport_operations VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (outbox_id),
  UNIQUE INDEX UQ_outbox_message_id(message_id),
  INDEX IX_outbox_dispatched(dispatched),
  INDEX IX_outbox_dispatched_at(dispatched_at)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TABLE credit_card(
  credit_card_id VARCHAR(36) NOT NULL,
  number_card VARCHAR(20),
  customer_id VARCHAR(36) NOT NULL,
  type_id VARCHAR(5),  
  expiration_at_utc DATETIME NOT NULL,
  amount_limit DECIMAL(10,2) UNSIGNED NOT NULL,
  ccv VARCHAR(3),
  PRIMARY KEY(credit_card_id),
  UNIQUE INDEX UQ_account_number(number_card)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;