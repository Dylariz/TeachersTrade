import 'package:flutter/material.dart';
import 'package:teach_trades/components/constants.dart';

import '../components/utilities/my_box.dart';
import '../components/utilities/my_tile.dart';

class DescktopScaffold extends StatefulWidget {
  const DescktopScaffold({super.key});

  @override
  State<DescktopScaffold> createState() => _DescktopScaffoldState();
}

class _DescktopScaffoldState extends State<DescktopScaffold> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: myDefaultBackground,
      appBar: myAppBar,
      body: Row(
        children: [
          // open drawer
          myDrawer,

          // rest of body
          Expanded(
            child: Column(
              children: [
                AspectRatio(
                  aspectRatio: 1,
                  child: SizedBox(
                    width: double.infinity,
                    child: GridView.builder(
                      itemCount: 4,
                      gridDelegate:
                          const SliverGridDelegateWithFixedCrossAxisCount(
                              crossAxisCount: 2),
                      itemBuilder: (context, index) {
                        return const MyBox();
                      },
                    ),
                  ),
                ),
                Expanded(
                    child: ListView.builder(
                  itemCount: 4,
                  itemBuilder: (context, index) {
                    return const MyTile();
                  },
                ))
              ],
            ),
          ),
          Expanded(
            flex: 1,
            child: Padding(
              padding: const EdgeInsets.all(10.0),
              child: Container(
                height: 500,
                width: 300,
                decoration: BoxDecoration(
                  color: Colors.grey[300],
                  borderRadius: BorderRadius.circular(12),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.grey.shade600,
                      blurRadius: 15,
                      spreadRadius: 1,
                      offset: const Offset(6, 6),
                    ),
                    const BoxShadow(
                      color: Colors.white,
                      blurRadius: 15,
                      spreadRadius: 1,
                      offset: Offset(-6, -6),
                    ),
                  ],
                ),
                child: const Center(
                    child: Text(
                  'I N V E N T O R Y',
                  style: TextStyle(
                    fontSize: 22,
                    fontFamily: 'Roboto',
                  ),
                )),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
