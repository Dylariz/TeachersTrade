import 'package:flutter/material.dart';
import 'package:teach_trades/responsive/desktop_scaffold.dart';
import 'package:teach_trades/responsive/mobile_scaffold.dart';
import 'package:teach_trades/responsive/responsive_layout.dart';
import 'package:teach_trades/responsive/tablet_scaffold..dart';

void main() => runApp(const MyApp());

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      debugShowCheckedModeBanner: false,
      home: ResponsiveLayout(
        mobileScaffold: MobileScaffold(),
        tabletScaffold: TabletScaffold(),
        desktopScaffold: DescktopScaffold(),
      ),
    );
  }
}
