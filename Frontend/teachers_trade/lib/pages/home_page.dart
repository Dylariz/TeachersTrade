import 'package:flutter/material.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[300],
      body: Column(
        // ignore: prefer_const_literals_to_create_immutables
        children: [
          const SizedBox(height: 100.0),
          const Center(
            child: Text(
              "Welcome to Teach Trades",
              style: TextStyle(fontSize: 25.0, fontWeight: FontWeight.bold),
            ),
          ),
          const SizedBox(height: 30.0),
          Row(
            children: [
              Container(
                margin: const EdgeInsets.only(left: 40),
                height: 200,
                width: 350,
                decoration: BoxDecoration(
                  color: Colors.grey[300],
                  borderRadius: BorderRadius.circular(30),
                  // ignore: prefer_const_literals_to_create_immutables
                  boxShadow: [
                    const BoxShadow(
                        color: Color.fromARGB(226, 158, 158, 158),
                        offset: Offset(5.0, 5.0),
                        blurRadius: 15,
                        spreadRadius: 1),
                    const BoxShadow(
                        color: Colors.white,
                        offset: Offset(-5.0, -5.0),
                        blurRadius: 15,
                        spreadRadius: 1)
                  ],
                ),
                child: Padding(
                  padding: const EdgeInsets.only(top: 30, left: 140, right: 30),
                  child: const Text(
                      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley. ",
                      style: TextStyle(fontSize: 15)),
                ),
              ),
              const SizedBox(width: 20),
              Container(
                margin: const EdgeInsets.only(left: 40),
                height: 200,
                width: 350,
                decoration: BoxDecoration(
                  color: Colors.grey[300],
                  borderRadius: BorderRadius.circular(30),
                  // ignore: prefer_const_literals_to_create_immutables
                  boxShadow: [
                    const BoxShadow(
                        color: Color.fromARGB(226, 158, 158, 158),
                        offset: Offset(4.0, 4.0),
                        blurRadius: 15,
                        spreadRadius: 1),
                    const BoxShadow(
                        color: Colors.white,
                        offset: Offset(-4.0, -4.0),
                        blurRadius: 15,
                        spreadRadius: 1)
                  ],
                ),
                child: Padding(
                  padding: const EdgeInsets.only(top: 30, left: 140, right: 30),
                  child: const Text(
                      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley. ",
                      style: TextStyle(fontSize: 15)),
                ),
              ),
            ],
          )
        ],
      ),
    );
  }
}
