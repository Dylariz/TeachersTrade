import 'package:avatar_glow/avatar_glow.dart';
import 'package:flutter/material.dart';

var myDefaultBackground = Colors.grey[300];

var myAppBar = AppBar(
  title: const Text(" T E A C H  T R A D E S "),
  actions: [
    IconButton(
        onPressed: () {},
        icon: const Icon(Icons.account_balance_wallet_rounded))
  ],
  backgroundColor: Colors.grey[600],
  shadowColor: Colors.grey[300],
  elevation: 20,
);

var myDrawer = Drawer(
  backgroundColor: Colors.grey[300],
  child: Column(
    // ignore: prefer_const_literals_to_create_immutables
    children: [
      const AvatarGlow(
        endRadius: 90,
        glowColor: Colors.grey,
        duration: Duration(seconds: 1),
        repeatPauseDuration: Duration(seconds: 2),
        child: Icon(Icons.favorite_rounded),
      ),
      Padding(
        padding: const EdgeInsets.all(10.0),
        child: ListTile(
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 2, color: Colors.black),
            borderRadius: BorderRadius.circular(20),
          ),
          leading: const Icon(
            Icons.home_rounded,
            color: Colors.black,
          ),
          title: const Text('D A S H B O A R D'),
          onTap: () {},
        ),
      ),
      Padding(
        padding: const EdgeInsets.all(10.0),
        child: ListTile(
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 2, color: Colors.black),
            borderRadius: BorderRadius.circular(20),
          ),
          leading: const Icon(Icons.chat_rounded, color: Colors.black),
          title: const Text('M E S S A G E'),
          onTap: () {},
        ),
      ),
      Padding(
        padding: const EdgeInsets.all(10.0),
        child: ListTile(
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 2, color: Colors.black),
            borderRadius: BorderRadius.circular(20),
          ),
          leading: const Icon(Icons.settings_rounded, color: Colors.black),
          title: const Text('S E T T I N G S'),
          onTap: () {},
        ),
      ),
      Padding(
        padding: const EdgeInsets.all(10.0),
        child: ListTile(
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 2, color: Colors.black),
            borderRadius: BorderRadius.circular(20),
          ),
          leading: const Icon(Icons.logout_rounded, color: Colors.black),
          title: const Text('L O G O U T'),
          onTap: () {},
        ),
      ),
      Divider(
        color: Colors.grey[900],
      ),
      const Padding(
        padding: EdgeInsets.all(5.0),
        child: Text(
          "D E V E L O P E D   B Y:",
          style: TextStyle(
              fontFamily: 'Roboto', fontSize: 14, fontWeight: FontWeight.bold),
        ),
      ),
      const Padding(
        padding: EdgeInsets.all(5.0),
        child: Text(
          "R A Y N E L Z",
          style: TextStyle(
            fontFamily: 'Roboto',
            fontSize: 14,
          ),
        ),
      ),
      Padding(
        padding: const EdgeInsets.only(
          left: 90,
          right: 90,
        ),
        child: Divider(
          color: Colors.grey[900],
        ),
      ),
      const Padding(
        padding: EdgeInsets.all(5.0),
        child: Text(
          "D Y L A R I Z",
          style: TextStyle(
            fontFamily: 'Roboto',
            fontSize: 14,
          ),
        ),
      ),
    ],
  ),
);
